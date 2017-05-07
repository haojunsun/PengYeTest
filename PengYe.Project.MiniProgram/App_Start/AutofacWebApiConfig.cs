using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Owin;
using PengYe.Project.Infrastructure;

namespace PengYe.Project.MiniProgram.App_Start
{
    public class AutofacWebApiConfig
    {
        //public static void Run()
        //{
        //    SetAutofacWebApi();
        //}

        public static void SetAutofacWebApi(IAppBuilder app)
        {
            ContainerBuilder builder = InfrastructureConfig.Init(app);
            
            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            //builder.RegisterType<ValueService>().As<IValueService>()
            //    .InstancePerApiRequest();
            var container = builder.Build();

            HttpConfiguration config = GlobalConfiguration.Configuration;
            // Set the WebApi dependency resolver.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}