using CustomOData.DataAccess.Abstractions;
using CustomOData.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CustomOData.Controllers
{
    public class EmployeesController : ODataController
    {
        public EmployeesController(IDBService dbService)
        {
            _dbService = dbService;
        }
        // GET: Employees
        [HttpGet]
        public IEnumerable<Employee> Get(ODataQueryOptions oDataQueryOptions)
        {
            IEnumerable<Employee> employees = new List<Employee>();
            using (IUnitOfWork unitOfWork = _dbService.UnitOfWork.Begin())
            {
                employees = _dbService.GetEmployees(oDataQueryOptions);
                unitOfWork.Commit();
            }
            
            if(oDataQueryOptions.SelectExpand != null)
            {
                Request.ODataProperties().SelectExpandClause = oDataQueryOptions?.SelectExpand.SelectExpandClause;
            }

            return employees;
        }

        private IDBService _dbService;
    }
}