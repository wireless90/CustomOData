using Autofac;
using CustomOData.App_Start.IOC.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomOData.App_Start.IOC
{
    public static class ContainerConfig
    {
        public static IContainer BuildContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterModule<WebApiModule>()
                .RegisterModule<DataAccessModule>();

            IContainer container = containerBuilder.Build();
            return container;
        }
    }
}