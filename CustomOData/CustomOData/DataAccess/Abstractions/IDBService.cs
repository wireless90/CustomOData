using CustomOData.Models;
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;

namespace CustomOData.DataAccess.Abstractions
{
    public interface IDBService
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<Employee> GetEmployees(ODataQueryOptions oDataQueryOptions);
    }
}