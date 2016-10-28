using System.Data.Entity;
using System.Web.UI.WebControls.Expressions;
using Market.Providers;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;
using MarketSimulator.Repository.Repo;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector.Diagnostics;

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
            
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			InitializeContainer(container);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
			container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
			container.Register<IMarketSimulatorContext, MarketSimulatorContext>(Lifestyle.Scoped);
			container.Register<DbContext, MarketSimulatorContext>(Lifestyle.Scoped);
			container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
	        container.Register<IOAuthAuthorizationServerProvider, SimpleAuthorizationServerProvider>();

			var marketRegistration = container.GetRegistration(typeof(IMarketSimulatorContext)).Registration;
			var dbContextRegistration = container.GetRegistration(typeof(DbContext)).Registration;
			marketRegistration.SuppressDiagnosticWarning(DiagnosticType.TornLifestyle, "Multiple instational intended.");
			dbContextRegistration.SuppressDiagnosticWarning(DiagnosticType.TornLifestyle, "Multiple instational intended.");
		}
    }
}