using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Ninject.Extensions.Conventions;
using TransactionManager.Commands;
using TransactionManager.CommandHandlers;
using DAL.Models;
using TransactionManager;
using UniotOfWorkInMVC.App_Start;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace UniotOfWorkInMVC.App_Start
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
            kernel.Bind<IAmDbContext>().To<UnitOfWorkInMVCContext>();
            kernel.Bind<IAmCommandsManager>().To<CommandsManager>();
            kernel.Bind(x => x.FromAssemblyContaining<IAmCommand>()
                  .SelectAllClasses()
                  .InheritedFrom(typeof(IAmCommand))
                  .BindSingleInterface());
            kernel.Bind(x => x.FromAssemblyContaining<IAmCommandHandler<IAmCommand>>()
                  .SelectAllClasses()
                  .InheritedFrom(typeof(IAmCommandHandler<>))
                  .BindSingleInterface());
        }
    }
}