using System.Collections.ObjectModel;
using Functional.Sharp.Errors;
using Functional.Sharp.Monads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Interface.Http.Extensions;

internal static class ResultExtensions
{
    internal static Results<NoContent, ProblemHttpResult> ToNoContentHttpResult<T>(
        this Result<T> result
    ) => result.Match<Results<NoContent, ProblemHttpResult>>(
        _ => TypedResults.NoContent(),
        err => TypedResults.Problem(err.Message)
    );

    internal static async Task<Results<NoContent, ProblemHttpResult>> ToNoContentHttpResult<T>(
        this Task<Result<T>> result
    ) => (await result).Match<Results<NoContent, ProblemHttpResult>>(
        _ => TypedResults.NoContent(),
        err => TypedResults.Problem(err.Message)
    );

    internal static Results<Ok<T>, ProblemHttpResult> ToHttpResult<T>(this Result<T> result)
        => result.Match<Results<Ok<T>, ProblemHttpResult>>(
            val => TypedResults.Ok<T>(val),
            err => TypedResults.Problem(err.Message)
        );

    internal static async Task<Results<Ok<T>, ProblemHttpResult>> ToHttpResult<T>(
        this Task<Result<T>> result)
        => (await result).Match<Results<Ok<T>, ProblemHttpResult>>(
            val => TypedResults.Ok<T>(val),
            err => TypedResults.Problem(err.Message)
        );

    private static readonly ReadOnlyDictionary<Type, int> DefaultErrorMappings = new(new Dictionary<Type, int>
    {
        { typeof(NotFoundError), 404 }
    });

    private static ProblemHttpResult DefaultErrorMapper(Error error)
        => TypedResults.Problem(
            title: error.Message,
            statusCode: DefaultErrorMappings.GetValueOrDefault(error.GetType(), 500));


    public static Results<TSuccess, ProblemHttpResult> MatchToHttp<TValue, TSuccess>(
        this Result<TValue> result,
        Func<TValue, TSuccess> map)
        where TSuccess : IResult
        => result.Match<Results<TSuccess, ProblemHttpResult>>(
            value => map(value),
            err => DefaultErrorMapper(err));
    
    public static async Task<Results<TSuccess, ProblemHttpResult>> MatchToHttp<TValue, TSuccess>(
        this Task<Result<TValue>> result,
        Func<TValue, TSuccess> map)
        where TSuccess : IResult
        => (await result).Match<Results<TSuccess, ProblemHttpResult>>(
            value => map(value),
            err => DefaultErrorMapper(err));


    internal static Results<Ok<TDestination>, ProblemHttpResult> ToHttpResult<TSource, TDestination>(
        this Result<TSource> result,
        Func<TSource, TDestination> mapper
    )
        => result.Match<Results<Ok<TDestination>, ProblemHttpResult>>(
            val => TypedResults.Ok(mapper(val)),
            err => TypedResults.Problem(err.Message)
        );

    internal static async Task<Results<Ok<TDestination>, ProblemHttpResult>> ToHttpResult<TSource,
        TDestination>(
        this Task<Result<TSource>> result,
        Func<TSource, TDestination> mapper)
        => (await result).Match<Results<Ok<TDestination>, ProblemHttpResult>>(
            val => TypedResults.Ok(mapper(val)),
            err => TypedResults.Problem(err.Message)
        );
}