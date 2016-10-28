using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Market.App_Start;

[assembly: OwinStartup(typeof(Market.Startup))]
namespace Market
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();

			WebApiConfig.Register(config);
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);

			SimpleInjectorWebApiInitializer.Initialize();
			OAuthInitializer.ConfigurationOAuth(app);
		}
	}
}