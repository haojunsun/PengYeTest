using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace PengYe.Project.Infrastructure
{
    public class InfrastructureConfig
    {
        /// <summary>
        /// 常规初始化组件
        /// </summary>
        public static ContainerBuilder Init()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IDependency))))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.GetInterfaces().Any(i => i == typeof(IDependencyPerRequest)))
                .AsSelf()
                .InstancePerRequest();
            return builder;
        }

        /// <summary>
        /// 在OWIN StartUp中对组件进行初始化
        /// </summary>
        public static ContainerBuilder Init(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie"
            });
            return Init();
        }
    }
}
