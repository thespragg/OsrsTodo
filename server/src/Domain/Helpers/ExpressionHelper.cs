using System.Linq.Expressions;

namespace Domain.Helpers;

internal static class ExpressionHelper
{
    internal static string GetPropertyPath<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            return GetPropertyPathFromMemberExpression(memberExpression);
        }
        throw new ArgumentException("Expression must be a member expression", nameof(expression));
    }

    private static string GetPropertyPathFromMemberExpression(MemberExpression? memberExpression)
    {
        var parts = new List<string>();
        while (memberExpression != null)
        {
            parts.Add(memberExpression.Member.Name);
            memberExpression = memberExpression.Expression as MemberExpression;
        }
        parts.Reverse();
        return string.Join(".", parts);
    }
	
    public static bool IsValidPropertyPath<TSource>(string propertyPath)
    {
        if (string.IsNullOrWhiteSpace(propertyPath))
        {
            return false;
        }

        var currentType = typeof(TSource);
        foreach (var propertyName in propertyPath.Split('.'))
        {
            var property = currentType.GetProperty(propertyName);
            if (property == null)
            {
                return false;
            }
            currentType = property.PropertyType;
        }
        return true;
    }
}