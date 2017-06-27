using BLL.Interface;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using Ninject;
using Ninject.Web.Common;
using ORM;
using System.Data.Entity;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }
        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }
        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                
                kernel.Bind<DbContext>().To<BlogModel>().InRequestScope();
            }
            else
            {
                kernel.Bind<DbContext>().To<BlogModel>().InSingletonScope();
            }

            kernel.Bind<IService<Users>>().To<BLService<Users>>();
            kernel.Bind<IService<Posts>>().To<BLService<Posts>>();
            kernel.Bind<IService<Comments>>().To<BLService<Comments>>();
            

            kernel.Bind<IRepository<Users>>().To<Repository<Users>>();            
            kernel.Bind<IRepository<Roles>>().To<Repository<Roles>>();
            kernel.Bind<IRepository<Comments>>().To<Repository<Comments>>();
            kernel.Bind<IRepository<Posts>>().To<Repository<Posts>>();

        }
    }
}
