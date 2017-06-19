using BLL;
using BLL.DTO;
using BLL.Interface;
using BLL.Services;
using DAL;
using DAL.DTO;
using DAL.Interfaces;
using Ninject;
using Ninject.Web.Common;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            kernel.Bind<IService<UserEntity>>().To<UserService>();
            kernel.Bind<IService<PostEntity>>().To<PostService>();
            kernel.Bind<IService<CommentEntity>>().To<CommentService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRepository<DalRole>>().To<RoleRepository>();
            kernel.Bind<IRepository<DalComment>>().To<CommentRepository>();
            kernel.Bind<IRepository<DalPost>>().To<PostReposytory>();

        }
    }
}
