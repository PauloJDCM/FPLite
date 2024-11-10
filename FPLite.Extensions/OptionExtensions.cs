using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using FPLite.Option;

namespace FPLite.Extensions;

public static class OptionExtensions
{
    /// <summary>
    /// Converts a nullable value (reference type) of type <typeparamref name="T"/> to an <see cref="Option{T}"/>.
    /// </summary>
    [Pure]
    public static Option<T> ToOption<T>(this T? value) where T : notnull =>
        value is null ? Option<T>.None() : Option<T>.Some(value);

    /// <summary>
    /// Converts a nullable value (value type) of type <typeparamref name="T"/> to an <see cref="Option{T}"/>.
    /// </summary>
    [Pure]
    public static Option<T> ToOption<T>(this T? value) where T : unmanaged =>
        value is null ? Option<T>.None() : Option<T>.Some((T)value);

    /// <summary>
    /// Converts a value of type <typeparamref name="TIn"/> to an <see cref="Option{TOut}"/>.
    /// </summary>
    [Pure]
    public static Option<TOut> AsOptionOf<TIn, TOut>(this TIn value)
        where TIn : notnull
        where TOut : notnull =>
        value is TOut cast ? Option<TOut>.Some(cast) : Option<TOut>.None();

    /// <summary>
    /// Tries to execute a function and returns a <see cref="Option{T}"/> with the result.
    /// </summary>
    [Pure]
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
    /// Tries to execute an async function and returns a <see cref="Option{T}"/> with the result.
    /// </summary>
    [Pure]
    public static async Task<Option<T>> TryOptionAsync<T>(Func<CancellationToken, Task<T>> func,
        CancellationToken ct = default) where T : notnull
    {
        try
        {
            return Option<T>.Some(await func(ct));
        }
        catch
        {
            return Option<T>.None();
        }
    }

    /// <summary>
    /// Tries to execute an action and returns a <see cref="Option{Exception}"/> with the result if an exception is thrown.
    /// </summary>
    public static Option<Exception> TryOption(Action action)
    {
        try
        {
            action();
            return Option<Exception>.None();
        }
        catch (Exception e)
        {
            return Option<Exception>.Some(e);
        }
    }

    /// <summary>
    /// Tries to execute an async action and returns a <see cref="Option{Exception}"/> with the result
    /// if an exception is thrown.
    /// </summary>
    public static async Task<Option<Exception>> TryOptionAsync(Func<CancellationToken, Task> action,
        CancellationToken ct = default)
    {
        try
        {
            await action(ct);
            return Option<Exception>.None();
        }
        catch (Exception e)
        {
            return Option<Exception>.Some(e);
        }
    }
}