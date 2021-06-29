using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using BookEventManager.Business.Logic;

namespace BookEventManager.UserInterface
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IReadData, ReadData>();
            container.RegisterType<IWriteData, WriteData>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}