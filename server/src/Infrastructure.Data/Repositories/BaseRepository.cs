using System.Linq.Expressions;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Models;
using Functional.Sharp.Errors;
using Functional.Sharp.Extensions;
using Functional.Sharp.Helpers;
using Functional.Sharp.Monads;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

internal abstract class BaseRepository<TSource, TEntity, TMapper>(
    OsrsTodoDbContext data
) : IBaseRepository<TEntity>
    where TSource : class
    where TEntity : class
    where TMapper : IEntityMapperBase<TSource, TEntity>, new()
{
    // ReSharper disable once ReplaceWithPrimaryConstructorParameter
    protected readonly OsrsTodoDbContext Context = data;
    private readonly TMapper _mapper = new();

    public async Task<Result<TEntity>> FindOne(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken
    ) => await Try.ExecuteAsync<Result<TEntity>>(async () =>
    {
        var mapper = _mapper.Create();
        var mappedPredicate = mapper.MapExpression<Expression<Func<TSource, bool>>>(predicate);
        var toEntityExpression = _mapper.ToEntityExpression();

        var entity = await Context
            .Set<TSource>()
            .AsNoTracking()
            .Where(mappedPredicate)
            .Select(toEntityExpression)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null) return new NotFoundError($"No {typeof(TSource)} found for the provided predicate.");
        return entity;
    }).FlattenAsync();

    public async Task<Result<TEntity>> FindOne(
        Expression<Func<TEntity, bool>> predicate,
        IncludeSpecification<TEntity> includes,
        CancellationToken cancellationToken
    ) => await Try.ExecuteAsync<Result<TEntity>>(async () =>
    {
        var mapper = _mapper.Create();
        var mappedPredicate = mapper.MapExpression<Expression<Func<TSource, bool>>>(predicate);
        var toEntityExpression = _mapper.ToEntityExpression();

        var query = Context
            .Set<TSource>()
            .AsNoTracking()
            .Where(mappedPredicate);

        var sourceIncludes = IncludeSpecification<TSource>.From(includes);
        query = sourceIncludes.ApplyIncludes(query);

        var entity = await query
            .Select(toEntityExpression)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null) return new NotFoundError($"No {typeof(TSource)} found for the provided predicate.");
        return entity;
    }).FlattenAsync();

    public async Task<Result<IEnumerable<TEntity>>> Find(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken
    ) => await Try.ExecuteAsync(async () =>
    {
        var mapper = _mapper.Create();
        var mappedPredicate = mapper.MapExpression<Expression<Func<TSource, bool>>>(predicate);
        var toEntityExpression = _mapper.ToEntityExpression();

        return (await Context
                .Set<TSource>()
                .AsNoTracking()
                .Where(mappedPredicate)
                .Select(toEntityExpression)
                .ToArrayAsync(cancellationToken))
            .AsEnumerable();
    });

    public async Task<Result<IEnumerable<TEntity>>> Find(
        Expression<Func<TEntity, bool>> predicate,
        IncludeSpecification<TEntity> includes,
        CancellationToken cancellationToken
    ) => await Try.ExecuteAsync(async () =>
    {
        var mapper = _mapper.Create();
        var mappedPredicate = mapper.MapExpression<Expression<Func<TSource, bool>>>(predicate);
        var toEntityExpression = _mapper.ToEntityExpression();

        var query = Context
            .Set<TSource>()
            .AsNoTracking()
            .Where(mappedPredicate);

        var sourceIncludes = IncludeSpecification<TSource>.From(includes);
        query = sourceIncludes.ApplyIncludes(query);

        return (await query
                .Select(toEntityExpression)
                .ToArrayAsync(cancellationToken)
            ).AsEnumerable();
    });
    
    public async Task<Result<TKey>> Create<TKey>(
        TEntity createEntity,
        Expression<Func<TEntity, TKey>> keySelector,
        CancellationToken cancellationToken = default
    ) => await Try.ExecuteAsync(async () =>
    {
        var mapper = _mapper.Create();
        var mappedPredicate = mapper.MapExpression<Expression<Func<TSource, TKey>>>(keySelector);

        var toRecordExpression = _mapper.ToRecordExpression().Compile();
        var record = toRecordExpression(createEntity);
        
        await Context.Set<TSource>().AddAsync(record, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedPredicate.Compile()(record);
    });

}