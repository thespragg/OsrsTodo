using System.Linq.Expressions;
using AutoMapper;

namespace Infrastructure.Data.Extensions;

internal static class MappingExpressionExtensions
{
    internal static IMappingExpression<TSource, TDestination> MapMember<TSource, TDestination, TSourceMember>(
        this IMappingExpression<TSource, TDestination> expr,
        Expression<Func<TDestination, object?>> dest,
        Expression<Func<TSource, TSourceMember?>> source
    ) => expr.ForMember(dest, opt => opt.MapFrom(source));

    internal static IMappingExpression<TSource, TDestination> MapPath<TSource, TDestination, TSourceMember>(
        this IMappingExpression<TSource, TDestination> expr,
        Expression<Func<TDestination, object?>> dest,
        Expression<Func<TSource, TSourceMember?>> source
    ) => expr.ForPath(dest, opt => opt.MapFrom(source));
}