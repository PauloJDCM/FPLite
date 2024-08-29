using System;

namespace FPLite.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Converts a nullable value of type <typeparamref name="TIn"/> to an <see cref="IResult{TOut, TError}"/>.
    /// </summary>
    public static IResult<TOut, TError> AsResultOf<TIn, TOut, TError>(this TIn value, Func<TError> errorFunc)
        where TError : notnull
        where TOut : notnull =>
        value is TOut cast ? FPLite.Ok<TOut, TError>(cast) : FPLite.Err<TOut, TError>(errorFunc());

    /// <summary>
    /// Tries to execute a function and returns a <see cref="IResult{T, TError}"/>.
    /// </summary>
    public static IResult<T, TError> TryResult<T, TError>(Func<T> func, Func<Exception, TError> errorFunc)
        where T : notnull
        where TError : notnull
    {
        try
        {
            return FPLite.Ok<T, TError>(func());
        }
        catch (Exception e)
        {
            return FPLite.Err<T, TError>(errorFunc(e));
        }
    }

    /// <summary>
    /// Tries to execute a function and returns a <see cref="IResult{T, TError}"/>.
    /// It returns an error if an exception of type <typeparamref name="TException"/> is thrown.
    /// </summary>
    public static IResult<T, TError> TryResult<T, TException, TError>(Func<T> func, Func<TException, TError> errorFunc)
        where T : notnull
        where TError : notnull
        where TException : Exception
    {
        try
        {
            return FPLite.Ok<T, TError>(func());
        }
        catch (TException e)
        {
            return FPLite.Err<T, TError>(errorFunc(e));
        }
    }
}