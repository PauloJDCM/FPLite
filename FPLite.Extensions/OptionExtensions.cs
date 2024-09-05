using System;
using FPLite.Option;

namespace FPLite.Extensions;

public static class OptionExtensions
{
    /// <summary>
    /// Converts a nullable value (reference type) of type <typeparamref name="T"/> to an <see cref="Option{T}"/>.
    /// </summary>
    public static Option<T> ToOption<T>(this T? value) where T : notnull =>
        value is null ? Option<T>.None() : Option<T>.Some(value);
    
    /// <summary>
    /// Converts a nullable value (value type) of type <typeparamref name="T"/> to an <see cref="Option{T}"/>.
    /// </summary>
    public static Option<T> ToOption<T>(this T? value) where T : unmanaged =>
        value is null ? Option<T>.None() : Option<T>.Some((T)value);

    /// <summary>
    /// Converts a value of type <typeparamref name="TIn"/> to an <see cref="Option{TOut}"/>.
    /// </summary>
    public static Option<TOut> AsOptionOf<TIn, TOut>(this TIn value)
        where TIn : notnull
        where TOut : notnull =>
        value is TOut cast ? Option<TOut>.Some(cast) : Option<TOut>.None();

    /// <summary>
    /// Tries to execute a function and returns a <see cref="Option{T}"/> with the result.
    /// </summary>
    public static Option<T> TryOption<T>(Func<T> func) where T : notnull
    {
        try
        {
            return Option<T>.Some(func());
        }
        catch
        {
            return Option<T>.None();
        }
    }

    /// <summary>
    /// Tries to execute an action and returns a <see cref="Option{TError}"/> with the result if an exception is thrown.
    /// </summary>
    public static Option<TError> TryOption<TError>(Action action, Func<Exception, TError> failFunc)
        where TError : notnull
    {
        try
        {
            action();
            return Option<TError>.None();
        }
        catch (Exception e)
        {
            return Option<TError>.Some(failFunc(e));
        }
    }

    /// <summary>
    /// Tries to execute an action and returns a <see cref="Option{TError}"/> with the result
    /// if an exception of type <typeparamref name="TException"/> is thrown.
    /// </summary>
    public static Option<TError> TryOption<TException, TError>(Action action, Func<TException, TError> failFunc)
        where TError : notnull
        where TException : Exception
    {
        try
        {
            action();
            return Option<TError>.None();
        }
        catch (TException e)
        {
            return Option<TError>.Some(failFunc(e));
        }
    }
}