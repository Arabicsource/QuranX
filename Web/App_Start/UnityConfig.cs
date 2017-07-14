using System;
using Microsoft.Practices.Unity;
using QuranX.DomainClasses.Services;
using QuranX.DomainClasses;

namespace QuranX.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            ServicesRegistration.Register(new ServicesRegistrationCallback(container));
        }
    }

    class ServicesRegistrationCallback : IServicesRegistrationCallback
    {
        private readonly IUnityContainer Container;

        public ServicesRegistrationCallback(IUnityContainer container)
        {
            this.Container = container;
        }

        public void RegisterScoped<TService, TImplementation>() 
            where TImplementation : TService
        {
            Container.RegisterType<TService, TImplementation>(new PerRequestLifetimeManager());
        }

        public void RegisterSingleton<TService, TImplementation>() 
            where TImplementation : TService
        {
            Container.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
        }

        public void RegisterTransient<TService, TImplementation>() 
            where TImplementation : TService
        {
            Container.RegisterType<TService, TImplementation>(new TransientLifetimeManager());
        }
    }
}
