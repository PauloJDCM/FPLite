using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using FPLite.Result;
using FPLite.Union;

namespace FPLite.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Converts a nullable value of type <typeparamref name="TIn"/> to an <see cref="Result{TOut, TError}"/>.
    /// </summary>
    [Pure]
    public static Result<TOut, TError> AsResultOf<TIn, TOut, TError>(this TIn value, Func<TError> errorFunc)
        where TError : notnull
        where TOut : notnull =>
        value is TOut cast ? Result<TOut, TError>.Ok(cast) : Result<TOut, TError>.Err(errorFunc());

    /// <summary>
    /// Tries to execute a function and returns a <see cref="Result{T, Exception}"/>.
    /// </summary>
    [Pure]
    public static Result<T, Exception> TryResult<T>(Func<T> func)
        where T : notnull
    {
        try
        {
            return Result<T, Exception>.Ok(func());
        }
        catch (Exception e)
        {
            return Result<T, Exception>.Err(e);
        }
    }

    /// <summary>
    /// Tries to execute an async function and returns a <see cref="Result{T, Exception}"/>.
    /// </summary>
    [Pure]
    public static async Task<Result<T, Exception>> TryResultAsyncTask<T>(Func<CancellationToken, Task<T>> func,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        try
        {
            return Result<T, Exception>.Ok(await func(cancellationToken));
        }
        catch (Exception e)
        {
            return Result<T, Exception>.Err(e);
        }
    }

    /// <summary>
    /// Tries to execute an async function and returns a <see cref="Result{T, Exception}"/>.
    /// </summary>
    [Pure]
    public static async ValueTask<Result<T, Exception>> TryResultAsyncValue<T>(
        Func<CancellationToken, ValueTask<T>> func,
        CancellationToken cancellationToken = default)
        where T : notnull
    {
        try
        {
            return Result<T, Exception>.Ok(await func(cancellationToken));
        }
        catch (Exception e)
        {
            return Result<T, Exception>.Err(e);
        }
    }

    /// <summary>
    /// Tries to execute a function and returns a <see cref="Result{T, Union{TException, Exception}}"/>.
    /// </summary>
    [Pure]
    public static Result<T, Union<TException, Exception>> TryResult<T, TException>(Func<T> func)
        where T : notnull
        where TException : Exception
    {
        try
        {
            return Result<T, Union<TException, Exception>>.Ok(func());
        }
        catch (TException e)
        {
            return Result<T, Union<TException, Exception>>.Err(Union<TException, Exception>.U1(e));
        }
        catch (Exception e)
        {
            return Result<T, Union<TException, Exception>>.Err(Union<TException, Exception>.U2(e));
        }
    }

    /// <summary>
    /// Tries to execute an async function and returns a <see cref="Result{T, Union{TException, Exception}}"/>.
    /// </summary>
    [Pure]
    public static async Task<Result<T, Union<TException, Exception>>> TryResultAsyncTask<T, TException>(
        Func<CancellationToken, Task<T>> func,
        CancellationToken cancellationToken = default)
        where T : notnull
        where TException : Exception
    {
        try
        {
            return Result<T, Union<TException, Exception>>.Ok(await func(cancellationToken));
        }
        catch (TException e)
        {
            return Result<T, Union<TException, Exception>>.Err(Union<TException, Exception>.U1(e));
        }
        catch (Exception e)
        {
            return Result<T, Union<TException, Exception>>.Err(Union<TException, Exception>.U2(e));
        }
    }

    /// <summary>
    /// Tries to execute an async function and returns a <see cref="Result{T, Union{TException, Exception}}"/>.
    /// </summary>
    [Pure]
    public static async ValueTask<Result<T, Union<TException, Exception>>> TryResultAsyncValue<T, TException>(
        Func<CancellationToken, ValueTask<T>> func,
        CancellationToken cancellationToken = default)
        where T : notnull
        where TException : Exception
    {
        try
        {
            return Result<T, Union<TException, Exception>>.Ok(await func(cancellationToken));
        }
        catch (TException e)
        {
            return Result<T, Union<TException, Exception>>.Err(Union<TException, Exception>.U1(e));
        }
        catch (Exception e)
        {
            return Result<T, Union<TException, Exception>>.Err(Union<TException, Exception>.U2(e));
        }
    }
}