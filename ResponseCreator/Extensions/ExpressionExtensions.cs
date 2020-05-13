using System;
using System.Linq.Expressions;

namespace ResponseCreator.Extensions
{
    internal static class ExpressionExtensions
    {
        internal static string GetName<T>(this Expression<Func<T>> action)
        {
            return GetNameFromMemberExpression(action.Body);
        }

        internal static string GetNameFromMemberExpression(this Expression expression)
        {
            switch (expression)
            {
                case MemberExpression memberExpression:
                {
                    return memberExpression.Member.Name;
                }
                case UnaryExpression unaryExpression:
                {
                    return GetNameFromMemberExpression(unaryExpression.Operand);
                }
                case LambdaExpression lambdaExpression:
                {
                    return GetNameFromMemberExpression(lambdaExpression.Body);
                }
                default:
                    return "MemberNameUnknown";
            }
        }
    }
}
