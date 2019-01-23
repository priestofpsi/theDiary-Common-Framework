using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /*
    public static class ActivatorHelper
    {
        private Delegate CreateExpression(Type objectType, string expression)
        {
            // TODO - add caching
            LambdaExpression lambdaExpression = System.Linq.Dynamic.DynamicExpression.ParseLambda((objectType, typeof(bool), expression);
            Delegate func = lambdaExpression.Compile();

            return func;
        }

        public static Expression<Func<bool, object>> CreateInstance(Type targetType, string constructorExpression)
        {
            var ctor = Expression.New(targetType);
            var lambda = System.Linq.Dynamic.DynamicExpression.ParseLambda(
            object instance = delegateWithConstructor.DynamicInvoke();

            var dynamicLambda = System.Linq.Dynamic.DynamicExpression.ParseLambda(typeof(object), targetType, constructorExpression);
            var displayValueParam = Expression.Parameter(typeof(bool), "displayValue");
            var ctor = Expression.New(targetType, dynamicLambda);
            var displayValueProperty = targetType.GetProperty("DisplayValue");
            var displayValueAssignment = Expression.Bind(
                displayValueProperty, displayValueParam);
            var memberInit = Expression.MemberInit( ctor, displayValueAssignment);

            return Expression.Lambda<Func<bool, object>>(memberInit, displayValueParam);
        }
    }*/
}
