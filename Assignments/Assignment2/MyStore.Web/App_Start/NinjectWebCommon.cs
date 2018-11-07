[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MyStore.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(MyStore.Web.App_Start.NinjectWebCommon), "Stop")]

namespace MyStore.Web.App_Start
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MyStore.Domain;
    using MyStore.Persistance;
    using MyStore.Persistance.Repositories;
    using MyStore.Persistance.Repositories.Interfaces;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind <IApplicationDBContext>().To<ApplicationDBContext>();
                kernel.Bind<IProductCategoryRepository>().To<ProductCategoryRepository>();
                kernel.Bind<IProductRepository>().To<ProductRepository>();
                kernel.Bind<IOrderRepository>().To<OrderRepository>();
                kernel.Bind<IOrderItemRepository>().To<OrderItemRepository>();
                kernel.Bind<IOrderStatusRepository>().To<OrderStatusRepository>();
                kernel.Bind<IDeliveryMethodRepository>().To<DeliveryMethodRepository>();

                //kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
                //kernel.Bind<UserManager<ApplicationUser>>().ToSelf();

                //kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

                //kernel.Bind<ApplicationSignInManager>().ToMethod((context) =>
                //{
                //    var cbase = new HttpContextWrapper(HttpContext.Current);
                //    return cbase.GetOwinContext().Get<ApplicationSignInManager>();
                //});

                //kernel.Bind<ApplicationUserManager>().ToSelf();


                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }
    }
}