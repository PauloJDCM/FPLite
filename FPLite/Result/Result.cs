using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using FPLite.Union;

namespace FPLite.Result;

public sealed class ResultUnwrapException<T, TError> : UnwrapException
{
    private const string ErrorMessage = "Called Result<{0}, {1}>.Unwrap() on Error!";

    public ResultUnwrapException() : base(string.Format(ErrorMessage, typeof(T), typeof(TError)))
    {
    }
}

public enum ResultType : byte
{
    NotSet,
    Ok,
    Err
}

public readonly record struct Result<T, TError>(
    T? Value = default,
    TError? Error = default,
    ResultType Type = ResultType.NotSet)
    where T : notnull where TError : notnull
{
    /// <summary>
    /// Creates a <see cref="Result{T, TError}"/> with the given value.
    /// </summary>
    [Pure]
    public static Result<T, TError> Ok([DisallowNull] T value) => new(Value: value, Type: ResultType.Ok);

    /// <summary>
    /// Creates a <see cref="Result{T, TError}"/> with the given error.
    /// </summary>
    [Pure]
    public static Result<T, TError> Err([DisallowNull] TError error) => new(Error: error, Type: ResultType.Err);

    /// <summary>
    /// Applies the appropriate function depending on the type of <see cref="Result{T, TError}" />.
    /// </summary>
    [Pure]
    public TResult Match<TResult>(Func<T, TResult> okFunc, Func<TError, TResult> errFunc) => Type switch
    {
        ResultType.Ok => okFunc(Value!),
        ResultType.Err => errFunc(Error!),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    /// <summary>
    /// Applies the appropriate async function depending on the type of <see cref="Result{T, TError}" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    [Pure]
    public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> okFunc,
        Func<TError, CancellationToken, Task<TResult>> errFunc, CancellationToken ct = default) => Type switch
    {
        ResultType.Ok => await okFunc(Value!, ct),
        ResultType.Err => await errFunc(Error!, ct),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    /// <summary>
    /// Applies the appropriate action depending on the type of <see cref="Result{T, TError}" />.
    /// </summary>
    public void Match(Action<T> okAct, Action<TError> errAct)
    {
        switch (Type)
        {
            case ResultType.Ok:
                okAct(Value!);
                break;
            case ResultType.Err:
                errAct(Error!);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }

    /// <summary>
    /// Applies the appropriate async action depending on the type of <see cref="Result{T, TError}" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    public async Task MatchAsync(Func<T, CancellationToken, Task> okAct,
        Func<TError, CancellationToken, Task> errAct,
        CancellationToken ct = default)
    {
        switch (Type)
        {
            case ResultType.Ok:
                await okAct(Value!, ct);
                break;
            case ResultType.Err:
                await errAct(Error!, ct);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }

    /// <summary>
    /// Applies the function if <see cref="Result{T, TError}" /> is <see cref="ResultType.Ok" />.
    /// </summary>
    [Pure]
    public Result<TResult, TError> Bind<TResult>(Func<T, TResult> okFunc)
        where TResult : notnull =>
        Type switch
        {
            ResultType.Ok => new(Value: okFunc(Value!), Type: ResultType.Ok),
            ResultType.Err => new(Error: Error, Type: ResultType.Err),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    [Pure]
    public async Task<Result<TResult, TError>> BindAsync<TResult>(
        Func<T, CancellationToken, Task<TResult>> okFunc,
        CancellationToken ct = default)
        where TResult : notnull =>
        Type switch
        {
            ResultType.Ok => new(Value: await okFunc(Value!, ct), Type: ResultType.Ok),
            ResultType.Err => new(Error: Error, Type: ResultType.Err),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    /// <summary>
    /// Gives the value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Ok" />.
    /// <br/>
    /// Throws a <see cref="ResultUnwrapException{T, TError}" /> if <see cref="Result{T, TError}" /> is <see cref="ResultType.Err" />.
    /// </summary>
    [Pure]
    public T Unwrap() => Type switch
    {
        ResultType.Ok => Value!,
        ResultType.Err => throw new ResultUnwrapException<T, TError>(),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    /// <summary>
    /// Gives the value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Ok" />.
    /// <br/>
    /// Returns the function value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Err" />.
    /// </summary>
    [Pure]
    public T UnwrapOr(Func<TError, T> func) => Type switch
    {
        ResultType.Ok => Value!,
        ResultType.Err => func(Error!),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };
    
    /// <summary>
    /// Gives the value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Ok" />.
    /// <br/>
    /// Returns the async function value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Err" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    [Pure]
    public async Task<T> UnwrapOrAsync(Func<TError, CancellationToken, Task<T>> func,
        CancellationToken ct = default) => Type switch
    {
        ResultType.Ok => Value!,
        ResultType.Err => await func(Error!, ct),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    /// <summary>
    /// Gives the value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Ok" />.
    /// <br/>
    /// Returns the function value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Err" />.
    /// </summary>
    /// <returns>A <see cref="Union{T,TOr}"/> with the value of <see cref="Result{T, TError}" /> or the function value.</returns>
    [Pure]
    public Union<T, TOr> UnwrapOr<TOr>(Func<TError, TOr> func)
        where TOr : notnull =>
        Type switch
        {
            ResultType.Ok => new(V1: Value!, Type: UnionType.T1),
            ResultType.Err => new(V2: func(Error!), Type: UnionType.T2),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };
    
    /// <summary>
    /// Gives the value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Ok" />.
    /// <br/>
    /// Returns the async function value if <see cref="Result{T, TError}" /> is <see cref="ResultType.Err" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    /// <returns>A <see cref="Union{T,TOr}"/> with the value of <see cref="Result{T, TError}" /> or the async function value.</returns>
    [Pure]
    public async Task<Union<T, TOr>> UnwrapOrAsync<TOr>(Func<TError, CancellationToken, Task<TOr>> func,
        CancellationToken ct = default)
        where TOr : notnull =>
        Type switch
        {
            ResultType.Ok => new(V1: Value!, Type: UnionType.T1),
            ResultType.Err => new(V2: await func(Error!, ct), Type: UnionType.T2),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };
}