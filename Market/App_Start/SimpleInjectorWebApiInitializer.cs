using System.Data.Entity;
using Market.Controllers;
using Market.Providers;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;
using MarketSimulator.Repository.Repo;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector.Diagnostics;
using SimpleInjector.Extensions.ExecutionContextScoping;

//[assembly: WebActivator.PostApplicationStartMethod(typeof(Market.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace Market.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    
    public static class SimpleInjectorWebApiInitializer
    {
	    /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
	    /// <param name="config"></param>
	    public static void Initialize(HttpConfiguration config)
        {
            var container = new Container();
	        container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

			InitializeContainer(container);

			container.Verify();

//			GlobalConfiguration.Configuration.DependencyResolver =
			config.DependencyResolver = 
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
			container.Register<IUnitOfWork, UnitOfWork>();
			container.Register<IMarketSimulatorContext, MarketSimulatorContext>(Lifestyle.Scoped);
			container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
	        //container.Register<IOAuthAuthorizationServerProvider, SimpleAuthorizationServerProvider>();

			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
		}
    }
}