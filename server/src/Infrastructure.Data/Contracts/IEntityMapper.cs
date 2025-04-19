using System.Linq.Expressions;
using AutoMapper;

namespace Infrastructure.Data.Contracts;

internal interface IEntityMapperBase<TSource, TDestination>
{
    void ApplyTo(IMapperConfigurationExpression expression);
    Expression<Func<TSource, TDestination>> ToEntityExpression();
    Expression<Func<TDestination, TSource>> ToRecordExpression();
}

internal interface IEntityMapper<TSource, TDestination> : IEntityMapperBase<TSource, TDestination>
{
}

internal abstract class EntityMapperBase<TSource, TDestination> : IEntityMapper<TSource, TDestination>
{
    public abstract void ApplyTo(IMapperConfigurationExpression expression);

    public Expression<Func<TSource, TDestination>> ToEntityExpression()
    {
        var mapper = new Mapper(new MapperConfiguration(ApplyTo));
        return dataModel => mapper.Map<TDestination>(dataModel);
    }

    public Expression<Func<TDestination, TSource>> ToRecordExpression()
    {
        var mapper = new Mapper(new MapperConfiguration(ApplyTo));
        return dataModel => mapper.Map<TSource>(dataModel);
    }
}