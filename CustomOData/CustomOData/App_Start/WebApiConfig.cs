using CustomOData.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CustomOData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ODataConventionModelBuilder oDataConventionModelBuilder = new ODataConventionModelBuilder();
            oDataConventionModelBuilder.EntitySet<Employee>("Employees");
            config.MapODataServiceRoute("ODataRoute", "api", oDataConventionModelBuilder.GetEdmModel());
            config.Count()
                .Filter()
                .OrderBy()
                .Expand()
                .Select()
                .MaxTop(null);
            config.EnableDependencyInjection();
            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
