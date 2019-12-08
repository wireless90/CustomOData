using CustomOData.DataAccess.Abstractions;
using CustomOData.Models;
using Microsoft.AspNet.OData.Query;
using SqlKata;
using SqlKata.Compilers;
using System.Collections.Generic;
using System.Data;
using Dapper;
using CustomOData.Logic.SqlKataHelper;

namespace CustomOData.DataAccess
{
    public class DBService : IDBService
    {
        public DBService(IUnitOfWork unitOfWork, Compiler compiler)
        {
            UnitOfWork = unitOfWork;
            _compiler = compiler;
            _dbConnection = UnitOfWork.GetDbConnection();
        }

        public IEnumerable<Employee> GetEmployees(ODataQueryOptions oDataQueryOptions)
        {
            int? top = oDataQueryOptions?.Top?.Value;
            int? skip = oDataQueryOptions?.Skip?.Value;
            Query query = new Query()
                .From(oDataQueryOptions.Context.NavigationSource.Name)
                .Skip(skip.HasValue ? skip.Value : 0)
                .Limit(top.HasValue ? top.Value : 0)
                .OrderByOData(oDataQueryOptions.OrderBy?.OrderByClause)
                .FilterOData(oDataQueryOptions.Filter?.FilterClause?.Expression)
                .Select(oDataQueryOptions.SelectExpand == null ? new string[] { "*" } : oDataQueryOptions.SelectExpand.RawSelect.Split(',')); 
                



            SqlResult sqlResult = _compiler.Compile(query);

            IEnumerable<Employee> employees =
                _dbConnection.Query<Employee>(sqlResult.Sql, sqlResult.NamedBindings, UnitOfWork.GetDbTransaction());

            return employees;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        private Compiler _compiler;
        private IDbConnection _dbConnection;
    }
}