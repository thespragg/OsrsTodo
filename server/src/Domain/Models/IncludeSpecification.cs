using System.Linq.Expressions;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public class IncludeSpecification<T> where T : class
{
    private readonly List<string> _includeStrings = [];
	
    public static IncludeSpecification<T> From<TSource>(IncludeSpecification<TSource> from) where TSource : class
    {
        var spec = new IncludeSpecification<T>();
        spec._includeStrings.AddRange(from.GetIncludes().Where(ExpressionHelper.IsValidPropertyPath<T>));
        return spec;
    }

    public IncludeSpecification<T> Include<TProperty>(Expression<Func<T, TProperty>> includeExpression)
    {
        string propertyPath = ExpressionHelper.GetPropertyPath(includeExpression);
        _includeStrings.Add(propertyPath);
        return this;
    }

    public IQueryable<T> ApplyIncludes(IQueryable<T> query)
    {
        return _includeStrings.Aggregate(query, (current, include) => current.Include(include));
    }

    public IEnumerable<string> GetIncludes() => _includeStrings;
}