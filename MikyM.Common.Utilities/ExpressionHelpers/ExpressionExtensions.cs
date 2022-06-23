﻿using System.Linq.Expressions;

namespace MikyM.Common.Utilities.ExpressionHelpers;

public static class ExpressionExtensions
{
    /// <summary>
    /// Convert a lambda expression for a getter into a setter
    /// </summary>
    public static Action<TEntity, TValue> GetSetter<TEntity, TValue>(this Expression<Func<TEntity, TValue>> expression)
    {
        var parameter = Expression.Parameter(typeof(TValue), "value");
        var setterLambda = Expression.Lambda<Action<TEntity, TValue>>(Expression.Assign(expression.Body, parameter),
            expression.Parameters[0], parameter);

        return setterLambda.Compile();
    }

    /// <summary>
    /// Gerts the value of a given <see cref="MemberExpression"/>.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static T? GetMemberExpressionValue<T>(this MemberExpression expression)
        => (T?)GetMemberExpressionValue(expression);

    /// <summary>
    /// Gerts the value of a given <see cref="MemberExpression"/>.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static object?GetMemberExpressionValue(this MemberExpression expression)
    {
        var dependencyChain = new List<MemberExpression>();
        var pointingExpression = expression;
        while (pointingExpression != null)
        {
            dependencyChain.Add(pointingExpression);
            pointingExpression = pointingExpression.Expression as MemberExpression;
        }

        if (dependencyChain.Last().Expression is not ConstantExpression baseExpression)
        {
            throw new Exception(
                $"Last expression {dependencyChain.Last().Expression} of dependency chain of {expression} is not a constant." +
                "Thus the expression value cannot be found.");
        }

        var resolvedValue = baseExpression.Value;

        for (var i = dependencyChain.Count; i > 0; i--)
        {
            var expr = dependencyChain[i - 1];
            resolvedValue = new PropOrField(expr.Member).GetValue(resolvedValue);
        }

        return resolvedValue;
    }
}
