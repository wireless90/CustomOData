using CustomOData.DataAccess.Abstractions;
using CustomOData.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public List<Employee> Get(ODataQueryOptions oDataQueryOptions)
        {
            List<Employee> employees = new List<Employee>();
            using (IUnitOfWork unitOfWork = _dbService.UnitOfWork.Begin())
            {
                employees = _dbService.GetEmployees(oDataQueryOptions).ToList();
                unitOfWork.Commit();
            }


            return employees;
        }

        private IDBService _dbService;
    }
}