using System;
using FPLite.Result;

namespace FPLite.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Converts a nullable value of type <typeparamref name="TIn"/> to an <see cref="Result{TOut, TError}"/>.
    /// </summary>
    public static Result<TOut, TError> AsResultOf<TIn, TOut, TError>(this TIn value, Func<TError> errorFunc)
        where TError : notnull
        where TOut : notnull =>
        value is TOut cast ? Result<TOut, TError>.Ok(cast) : Result<TOut, TError>.Err(errorFunc());

    /// <summary>
    /// Tries to execute a function and returns a <see cref="Result{T, TError}"/>.
    /// </summary>
    public static Result<T, TError> TryResult<T, TError>(Func<T> func, Func<Exception, TError> errorFunc)
        where T : notnull
        where TError : notnull
    {
        try
        {
            return Result<T, TError>.Ok(func());
        }
        catch (Exception e)
        {
            return Result<T, TError>.Err(errorFunc(e));
        }
    }

    /// <summary>
    /// Tries to execute a function and returns a <see cref="Result{T, TError}"/>.
    /// It returns an error if an exception of type <typeparamref name="TException"/> is thrown.
    /// </summary>
    public static Result<T, TError> TryResult<T, TException, TError>(Func<T> func, Func<TException, TError> errorFunc)
        where T : notnull
        where TError : notnull
        where TException : Exception
    {
        try
        {
            return Result<T, TError>.Ok(func());
        }
        catch (TException e)
        {
            return Result<T, TError>.Err(errorFunc(e));
        }
    }
}