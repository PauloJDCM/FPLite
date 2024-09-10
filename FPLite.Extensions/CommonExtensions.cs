using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace FPLite.Extensions;

public static class CommonExtensions
{
    /// <summary>
    /// Pipes the input value into the specified function, returning the result.
    /// </summary>
    [Pure]
    public static TResult Pipe<T, TResult>(this T input, Func<T, TResult> func) => func(input);

    /// <summary>
    /// Pipes the input value into the specified async function, returning the result.
    /// </summary>
    [Pure]
    public static async Task<TResult> PipeAsyncTask<T, TResult>(this T input,
        Func<T, CancellationToken, Task<TResult>> func, CancellationToken ct = default) =>
        await func(input, ct);

    /// <summary>
    /// Pipes the input value into the specified async function, returning the result.
    /// </summary>
    [Pure]
    public static async ValueTask<TResult> PipeAsyncValue<T, TResult>(this T input,
        Func<T, CancellationToken, ValueTask<TResult>> func, CancellationToken ct = default) =>
        await func(input, ct);

    /// <summary>
    /// Pipes the input value into the specified action.
    /// </summary>
    public static void Pipe<T>(this T input, Action<T> action) => action(input);

    /// <summary>
    /// Pipes the input value into the specified async action.
    /// </summary>
    public static async Task PipeAsyncTask<T>(this T input, Func<T, CancellationToken, Task> action,
        CancellationToken ct = default) => await action(input, ct);

    /// <summary>
    /// Pipes the input value into the specified async action.
    /// </summary>
    public static async ValueTask PipeAsyncValue<T>(this T input, Func<T, CancellationToken, ValueTask> action,
        CancellationToken ct = default) =>
        await action(input, ct);

    /// <summary>
    /// Returns an empty action.
    /// </summary>
    public static Action Ignore() => () =>
    {
        /* Empty action, does nothing */
    };

    /// <summary>
    /// Returns an empty function.
    /// </summary>
    public static void Ignore<T>(this T _)
    {
    }

    /// <summary>
    /// Returns an empty async action.
    /// </summary>
    public static Task IgnoreAsyncTask(this Task _) => Task.CompletedTask;

    /// <summary>
    /// Returns an empty async action.
    /// </summary>
    public static ValueTask IgnoreAsyncValue(this ValueTask _) => ValueTask.CompletedTask;
}