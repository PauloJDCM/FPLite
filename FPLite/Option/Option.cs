using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using FPLite.Union;

namespace FPLite.Option;

public sealed class OptionUnwrapException<T> : UnwrapException
{
    private const string ErrorMessage = "Called Option<{0}>.Unwrap() on None!";

    public OptionUnwrapException() : base(string.Format(ErrorMessage, typeof(T)))
    {
    }
}

public enum OptionType : byte
{
    NotSet,
    None,
    Some
}

public readonly record struct Option<T>(T? Value = default, OptionType Type = OptionType.NotSet)
    where T : notnull
{
    /// <summary>
    /// Creates a <see cref="Option{T}"/> with the given value.
    /// </summary>
    [Pure]
    public static Option<T> Some([DisallowNull] T value) => new(value, OptionType.Some);

    /// <summary>
    /// Creates a <see cref="Option{T}"/> with no value.
    /// </summary>
    [Pure]
    public static Option<T> None() => new(Type: OptionType.None);

    /// <summary>
    /// Applies the appropriate function depending on the type of <see cref="Option{T}" />.
    /// </summary>
    [Pure]
    public TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc) => Type switch
    {
        OptionType.Some => someFunc(Value!),
        OptionType.None => noneFunc(),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    /// <summary>
    /// Applies the appropriate async function depending on the type of <see cref="Option{T}" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    [Pure]
    public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> someFunc,
        Func<CancellationToken, Task<TResult>> noneFunc, CancellationToken ct = default) => Type switch
    {
        OptionType.Some => await someFunc(Value!, ct).ConfigureAwait(false),
        OptionType.None => await noneFunc(ct).ConfigureAwait(false),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    /// <summary>
    /// Applies the appropriate action depending on the type of <see cref="Option{T}" />.
    /// </summary>
    public void Match(Action<T> someAct, Action noneAct)
    {
        switch (Type)
        {
            case OptionType.Some:
                someAct(Value!);
                break;
            case OptionType.None:
                noneAct();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }

    /// <summary>
    /// Applies the appropriate async action depending on the type of <see cref="Option{T}" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    public Task MatchAsync(Func<T, CancellationToken, Task> someAct,
        Func<CancellationToken, Task> noneAct, CancellationToken ct = default)
    {
        switch (Type)
        {
            case OptionType.Some:
                return someAct(Value!, ct);
            case OptionType.None:
                return noneAct(ct);
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }

    /// <summary>
    /// Applies the function if <see cref="Option{T}" /> is <see cref="OptionType.Some" />.
    /// </summary>
    [Pure]
    public Option<TResult> Bind<TResult>(Func<T, TResult> someFunc)
        where TResult : notnull =>
        Type switch
        {
            OptionType.Some => new(someFunc(Value!), OptionType.Some),
            OptionType.None => new(Type: OptionType.None),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    /// <summary>
    /// Applies the async function if <see cref="Option{T}" /> is <see cref="OptionType.Some" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    [Pure]
    public async Task<Option<TResult>> BindAsync<TResult>(Func<T, CancellationToken, Task<TResult>> someFunc,
        CancellationToken ct = default)
        where TResult : notnull =>
        Type switch
        {
            OptionType.Some => new(await someFunc(Value!, ct), OptionType.Some),
            OptionType.None => new(Type: OptionType.None),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    /// <summary>
    /// Gives the value if <see cref="Option{T}" /> is <see cref="OptionType.Some" />.
    /// <br/>
    /// Throws a <see cref="OptionUnwrapException{T}" /> if <see cref="Option{T}" /> is <see cref="OptionType.None" />.
    /// </summary>
    [Pure]
    public T Unwrap() => Type switch
    {
        OptionType.Some => Value!,
        OptionType.None => throw new OptionUnwrapException<T>(),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    /// <summary>
    /// Gives the value if <see cref="Option{T}" /> is <see cref="OptionType.Some" />.
    /// <br/>
    /// Returns the function value if <see cref="Option{T}" /> is <see cref="OptionType.None" />.
    /// </summary>
    [Pure]
    public T UnwrapOr(Func<T> func) => Type switch
    {
        OptionType.Some => Value!,
        OptionType.None => func(),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    /// <summary>
    /// Gives the value if <see cref="Option{T}" /> is <see cref="OptionType.Some" />.
    /// <br/>
    /// Returns the async function value if <see cref="Option{T}" /> is <see cref="OptionType.None" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    [Pure]
    public Task<T> UnwrapOrAsync(Func<CancellationToken, Task<T>> func, CancellationToken ct = default) =>
        Type switch
        {
            OptionType.Some => Task.FromResult(Value!),
            OptionType.None => func(ct),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    /// <summary>
    /// Gives the value if <see cref="Option{T}" /> is <see cref="OptionType.Some" />.
    /// <br/>
    /// Returns the function value if <see cref="Option{T}" /> is <see cref="OptionType.None" />.
    /// </summary>
    /// <returns>A <see cref="Union{T,TOr}"/> with the value of <see cref="Option{T}" /> or the function value.</returns>
    [Pure]
    public Union<T, TOr> UnwrapOr<TOr>(Func<TOr> func)
        where TOr : notnull =>
        Type switch
        {
            OptionType.Some => new(V1: Value!, Type: UnionType.T1),
            OptionType.None => new(V2: func(), Type: UnionType.T2),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    /// <summary>
    /// Gives the value if <see cref="Option{T}" /> is <see cref="OptionType.Some" />.
    /// <br/>
    /// Returns the async function value if <see cref="Option{T}" /> is <see cref="OptionType.None" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    /// <returns>A <see cref="Union{T,TOr}"/> with the value of <see cref="Option{T}" /> or the async function value.</returns>
    [Pure]
    public async Task<Union<T, TOr>> UnwrapOrAsync<TOr>(Func<CancellationToken, Task<TOr>> func,
        CancellationToken ct = default)
        where TOr : notnull =>
        Type switch
        {
            OptionType.Some => new(V1: Value!, Type: UnionType.T1),
            OptionType.None => new(V2: await func(ct), Type: UnionType.T2),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };
}