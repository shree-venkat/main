using System;
using Server.Repo.Contexts;
using Microsoft.Practices.Unity;
using System.Data.Entity;

namespace Server.Business.Common
{
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> ContainerLoader = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            RegisterBusinessLayerTypes(container);
            RegisterDataLayerTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return ContainerLoader.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        private static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterTypes(
                            AllClasses.FromLoadedAssemblies(),
                            WithMappings.FromMatchingInterface,
                            WithName.Default);
        }

        private static void RegisterBusinessLayerTypes(IUnityContainer container)
        {
            //container.RegisterType<ICRMSuppliers, CRMSuppliersClient>();
        }

        private static void RegisterDataLayerTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, DatabaseContext>();
        }
    }
}