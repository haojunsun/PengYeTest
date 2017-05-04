using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using PengYe.Project.MiniProgram.App_Start;

[assembly: OwinStartup(typeof(PengYe.Project.MiniProgram.Startup))]

namespace PengYe.Project.MiniProgram
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyConfig.RegisterDependencies(app);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
