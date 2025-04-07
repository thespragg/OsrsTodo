using System.Linq.Expressions;
using AutoMapper;

namespace Infrastructure.Data.Contracts;

internal interface IEntityMapperBase<TSource, TDestination>
{
    Mapper Create();
    Expression<Func<TSource, TDestination>> ToEntityExpression();
    Expression<Func<TDestination, TSource>> ToRecordExpression();
}

internal interface IEntityMapper<TSource, TDestination> : IEntityMapperBase<TSource, TDestination>
{
}

internal abstract class EntityMapperBase<TSource, TDestination> : IEntityMapper<TSource, TDestination>
{
    public abstract Mapper Create();

    public Expression<Func<TSource, TDestination>> ToEntityExpression()
    {
        var mapper = Create();
        return dataModel => mapper.Map<TDestination>(dataModel);
    }

    public Expression<Func<TDestination, TSource>> ToRecordExpression()
    {
        var mapper = Create();
        return dataModel => mapper.Map<TSource>(dataModel);
    }
}