using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Module = Autofac.Module;

namespace CustomOData.App_Start.IOC.Modules
{
    public class WebApiModule:Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            //Example from
            //https://autofaccn.readthedocs.io/en/latest/integration/webapi.html#quick-start
            // Get your HttpConfiguration.
            HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            containerBuilder.RegisterWebApiFilterProvider(httpConfiguration);

            // OPTIONAL: Register the Autofac model binder provider.
            containerBuilder.RegisterWebApiModelBinderProvider();

            base.Load(containerBuilder);
        }
    }
}