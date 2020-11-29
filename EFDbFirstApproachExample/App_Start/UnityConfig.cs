using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Company.ServiceContracts;
using Company.ServiceLayer;
using Company.RepositoryContracts;
using Company.RepositoryLayer;

namespace EFDbFirstApproachExample
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IProductsService, ProductsService>();
            container.RegisterType<IProductsRepository, ProductsRepository>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}