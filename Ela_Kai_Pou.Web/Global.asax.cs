using Autofac;
using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Servises.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Ela_Kai_Pou.Entities;

namespace Ela_Kai_Pou.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            // manual registration of types;
            builder.RegisterType<CoffeeRepository>().As<ICoffeeRepository>();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<OrderItemRepository>().As<IOrderItemRepository>();
            builder.RegisterType<CartRepository>().As<ICartRepository>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<CoffeeShopDb>().As<ICoffeeShopDb>().InstancePerLifetimeScope();


            builder.RegisterFilterProvider();
            // For property injection using Autofac
            // builder.RegisterType<QuoteService>().PropertiesAutowired();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
