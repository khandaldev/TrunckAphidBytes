using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web
{
    public static class ModelStateHelper
    {
        public static void RemoveFor<TModel>(this ModelStateDictionary modelState,
                                         Expression<Func<TModel, object>> expression )
        {

              
            string expressionText = GetExpressionText(expression);

             
            foreach (var ms in modelState.ToArray())
            {

                if ((ms.Key.StartsWith(expressionText + ".") || ms.Key == expressionText || ms.Key.StartsWith(expressionText + "[")))
                {
                        modelState.Remove(ms);
                   
                }
            }
        }

        static public string GetExpressionText(LambdaExpression p)
        {
            if (p.Body.NodeType == ExpressionType.Convert || p.Body.NodeType == ExpressionType.ConvertChecked)
            {
                p = Expression.Lambda(((UnaryExpression)p.Body).Operand,
                    p.Parameters);
            }
            return ExpressionHelper.GetExpressionText(p);
        }
    }
}