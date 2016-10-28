using System;
using System.Web.Http;
using Market.Providers;
using MarketSimulator.Repository.IRepo;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

//[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Market.App_Start.OAuthInitializer), "ConfigurationOAuth", Order = 2)]
namespace Market.App_Start
{
	public class OAuthInitializer
	{
		public static void ConfigurationOAuth(IAppBuilder app)
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