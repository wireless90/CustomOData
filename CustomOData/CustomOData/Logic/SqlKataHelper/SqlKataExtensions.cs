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

        public static Query FilterOData(this Query query, SingleValueNode singleValueNode)
        {
            if (singleValueNode == null)
            {
                return query;
            }

            if(singleValueNode.Kind == QueryNodeKind.BinaryOperator)
            {   
                BinaryOperatorNode binaryOperatorNode = singleValueNode as BinaryOperatorNode;
                query = ProcessBinaryOperatorNode(query, binaryOperatorNode);
            }

            return query;
        }

        private static Query ProcessBinaryOperatorNode(Query query, BinaryOperatorNode binaryOperatorNode)
        {
            if(binaryOperatorNode.OperatorKind == BinaryOperatorKind.Equal)
            {
                SingleValueNode singleValueNodeLeft = binaryOperatorNode.Left;
                SingleValueNode singleValueNodeRight = binaryOperatorNode.Right;

                if(singleValueNodeLeft.Kind == QueryNodeKind.SingleValuePropertyAccess)
                {
                    SingleValuePropertyAccessNode singleValuePropertyAccessNodeLeft = singleValueNodeLeft as SingleValuePropertyAccessNode;
                    
                    if(binaryOperatorNode.Right.Kind == QueryNodeKind.Constant)
                    {
                        ConstantNode constantNodeRight = singleValueNodeRight as ConstantNode;
                        return query.Where(singleValuePropertyAccessNodeLeft.Property.Name, constantNodeRight.Value);
                    }
                }
            }

            return query;
        }
    }
}