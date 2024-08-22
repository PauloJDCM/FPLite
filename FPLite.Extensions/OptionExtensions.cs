using System;
using FPLite.Option;

namespace FPLite.Extensions;

public static class OptionExtensions
{
    /// <summary>
    /// Converts a value of type <typeparamref name="TIn"/> to an <see cref="IOption{TOut}"/>.
    /// </summary>
    public static IOption<TOut> AsOptionOf<TIn, TOut>(this TIn value) =>
        value is TOut cast ? FPLite.Some(cast) : FPLite.None<TOut>();

    /// <summary>
    /// Tries to execute a function and returns a <see cref="IOption{T}"/> with the result.
    /// </summary>
    public static IOption<T> TryOption<T>(Func<T> func)
    {
        try
        {
            return FPLite.Some(func());
        }
        catch
        {
            return FPLite.None<T>();
        }
    }

    /// <summary>
    /// Tries to execute an action and returns a <see cref="IOption{TError}"/> with the result if an exception is thrown.
    /// </summary>
    public static IOption<TError> TryOption<TError>(Action action, Func<Exception, TError> failFunc)
    {
        try
        {
            action();
            return FPLite.None<TError>();
        }
        catch (Exception e)
        {
            return FPLite.Some(failFunc(e));
        }
    }

    /// <summary>
    /// Tries to execute an action and returns a <see cref="IOption{TError}"/> with the result
    /// if an exception of type <typeparamref name="TException"/> is thrown.
    /// </summary>
    public static IOption<TError> TryOption<TException, TError>(Action action, Func<TException, TError> failFunc)
        where TException : Exception
    {
        try
        {
            action();
            return FPLite.None<TError>();
        }
        catch (TException e)
        {
            return FPLite.Some(failFunc(e));
        }
    }
}