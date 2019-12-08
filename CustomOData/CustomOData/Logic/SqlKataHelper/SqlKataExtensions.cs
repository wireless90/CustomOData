using Microsoft.AspNet.OData.Query;
using Microsoft.OData.UriParser;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomOData.Logic.SqlKataHelper
{
    public static class SqlKataExtensions
    {
        public static Query OrderByOData(this Query query, OrderByClause orderByClause)
        {
            if (orderByClause == null)
            {
                return query;
            }

            SingleValuePropertyAccessNode singleValuePropertyAccessNode = (SingleValuePropertyAccessNode) orderByClause.Expression;

            query = orderByClause.Direction == OrderByDirection.Ascending ?
                query.OrderBy(singleValuePropertyAccessNode.Property.Name) : 
                query.OrderByDesc(singleValuePropertyAccessNode.Property.Name);

            return OrderByOData(query, orderByClause.ThenBy);
        }


    }
}