using Autofac;
using Autofac.Integration.Mvc;
using EcommerceCore.Websites.Modules;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EcommerceCore.Websites.Areas.Admin.Mapping;
using EcommerceCore.Websites.Mapping;

namespace EcommerceCore.Websites
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Autofac Configuration
            var builder = new ContainerBuilder();

            //builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AutoMapper.Mapper.Initialize(m => m.AddProfile<MappingAdminProfile>());

            //AutoMapper.Mapper.Initialize(m => m.AddProfile<MappingProfile>());
        }
    }
}
