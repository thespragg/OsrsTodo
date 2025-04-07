using System.Linq.Expressions;
using Domain.Models;
using Functional.Sharp.Monads;

namespace Domain.Contracts.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public Task<Result<TEntity>> FindOne(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken
    );

    public Task<Result<TEntity>> FindOne(
        Expression<Func<TEntity, bool>> predicate,
        IncludeSpecification<TEntity> includeBuilder,
        CancellationToken cancellationToken
    );

    public Task<Result<IEnumerable<TEntity>>> Find(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken
    );

    public Task<Result<IEnumerable<TEntity>>> Find(
        Expression<Func<TEntity, bool>> predicate,
        IncludeSpecification<TEntity> includeBuilder,
        CancellationToken cancellationToken
    );

    Task<Result<TKey>> Create<TKey>(
        TEntity createEntity,
        Expression<Func<TEntity, TKey>> keySelector,
        CancellationToken cancellationToken = default
    );
}