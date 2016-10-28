using System.Data.Entity;
using Market.Providers;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;
using MarketSimulator.Repository.Repo;
using Microsoft.Owin.Security.OAuth;

[assembly: WebActivator.PostApplicationStartMethod(typeof(Market.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace Market.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            //container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
			container.Register<IUnitOfWork, UnitOfWork>();
			container.Register<IMarketSimulatorContext, MarketSimulatorContext>();
			container.Register<DbContext, MarketSimulatorContext>();
			container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
	        container.Register<IOAuthAuthorizationServerProvider, SimpleAuthorizationServerProvider>();
        }
    }
}