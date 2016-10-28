using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Market.App_Start;
using Market.Providers;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Repo;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(Market.Startup))]
namespace Market
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();

			SimpleInjectorWebApiInitializer.Initialize();
			ConfigurationOAuth(app);

			WebApiConfig.Register(config);
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);
		}

		public void ConfigurationOAuth(IAppBuilder app)
		{
			var unitOfWork = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnitOfWork)) as IUnitOfWork;

				OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
				{
					AllowInsecureHttp = true,
					TokenEndpointPath = new PathString("/token"),
					AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
					Provider = new SimpleAuthorizationServerProvider(unitOfWork)
				};

			app.UseOAuthAuthorizationServer(OAuthServerOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}