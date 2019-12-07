using CustomOData.DataAccess.Abstractions;
using CustomOData.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;

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
        public List<Employee> Get(ODataQueryOptions oDataQueryOptions)
        {
            List<Employee> employees = new List<Employee>();
            using (IUnitOfWork unitOfWork = _dbService.UnitOfWork.Begin())
            {
                employees = _dbService.GetEmployees(oDataQueryOptions).ToList();
                unitOfWork.Commit();
            }
            
            Request.ODataProperties().SelectExpandClause = oDataQueryOptions?.SelectExpand?.SelectExpandClause;

            return employees;
        }

        private IDBService _dbService;
    }
}