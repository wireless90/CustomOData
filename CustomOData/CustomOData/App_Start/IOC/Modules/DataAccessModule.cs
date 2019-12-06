using Autofac;
using CustomOData.DataAccess;
using CustomOData.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CustomOData.App_Start.IOC.Modules
{
    public class DataAccessModule:Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<SqlConnection>()
                .AsSelf();

            containerBuilder.Register(componentContext => {
                    IDbConnection dbConnection = componentContext.Resolve<SqlConnection>();
                    dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString();   
                    return dbConnection;
                })
                .AsSelf();

            containerBuilder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>();

            containerBuilder.RegisterType<DBService>()
                .As<IDBService>();

            base.Load(containerBuilder);
        }
    }
}