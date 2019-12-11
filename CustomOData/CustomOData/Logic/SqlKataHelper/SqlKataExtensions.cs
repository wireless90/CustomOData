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
            switch(binaryOperatorNode.OperatorKind)
            {
                case BinaryOperatorKind.Equal:
                case BinaryOperatorKind.NotEqual:
                case BinaryOperatorKind.GreaterThan:
                case BinaryOperatorKind.GreaterThanOrEqual:
                case BinaryOperatorKind.LessThan:
                case BinaryOperatorKind.LessThanOrEqual:

                    SingleValueNode singleValueNodeLeft = binaryOperatorNode.Left;
                    SingleValueNode singleValueNodeRight = binaryOperatorNode.Right;

                    if(singleValueNodeLeft.Kind == QueryNodeKind.Constant &&
                        singleValueNodeRight.Kind == QueryNodeKind.SingleValuePropertyAccess)
                    {

                        SingleValuePropertyAccessNode singleValuePropertyAccessNode = (SingleValuePropertyAccessNode)singleValueNodeRight;
                        ConstantNode constantNode = (ConstantNode)singleValueNodeLeft;

                        return query.Where(singleValuePropertyAccessNode.Property.Name, 
                            _binaryOperatorKindToSymbolDictionary[binaryOperatorNode.OperatorKind],
                            constantNode.Value);
                    }
                    else if(singleValueNodeLeft.Kind == QueryNodeKind.SingleValuePropertyAccess &&
                        singleValueNodeRight.Kind == QueryNodeKind.Constant)
                    {
                        SingleValuePropertyAccessNode singleValuePropertyAccessNode = (SingleValuePropertyAccessNode)singleValueNodeLeft;
                        ConstantNode constantNode = (ConstantNode)singleValueNodeRight;

                        return query.Where(singleValuePropertyAccessNode.Property.Name,
                            _binaryOperatorKindToSymbolDictionary[binaryOperatorNode.OperatorKind],
                            constantNode.Value);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
            }
            
            return query;
        }

        private static Dictionary<BinaryOperatorKind, String> _binaryOperatorKindToSymbolDictionary = new Dictionary<BinaryOperatorKind, String>()
        {
            {BinaryOperatorKind.Equal,  "=" },
            {BinaryOperatorKind.GreaterThan, ">" },
            {BinaryOperatorKind.GreaterThanOrEqual, ">=" },
            {BinaryOperatorKind.LessThan, "<" },
            {BinaryOperatorKind.LessThanOrEqual, "<=" },
            {BinaryOperatorKind.NotEqual, "!=" },
        };
    }
}