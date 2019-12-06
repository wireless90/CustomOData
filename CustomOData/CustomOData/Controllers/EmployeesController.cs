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
        // GET: Employees
        [HttpGet]
        [EnableQuery]
        public List<Employee> Get(ODataQueryOptions oDataQueryOptions)
        {
            return new List<Employee>() { new Employee() { Age = 10, Id = 1, Name = "Razali" } };
        }
    }
}