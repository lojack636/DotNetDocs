using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;

namespace WebApplication.App_Start
{
    public class AutofacIoc
    {
        /// <summary>
        /// Autofac依赖注入
        /// </summary>
        public static void AutofacRegister()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GPEntities>();
            var baseType = typeof(IDependency);
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            builder.RegisterControllers(assemblies).PropertiesAutowired();
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract)
                   .Where(c => c.Name.EndsWith("Service"))
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .PropertiesAutowired()
                   .InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}