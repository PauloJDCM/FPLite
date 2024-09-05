using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
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
    [Pure]
    public static Result<T, TError> Ok([DisallowNull] T value) => new(Value: value, Type: ResultType.Ok);

    [Pure]
    public static Result<T, TError> Err([DisallowNull] TError error) => new(Error: error, Type: ResultType.Err);

    [Pure]
    public TResult Match<TResult>(Func<T, TResult> okFunc, Func<TError, TResult> errFunc) => Type switch
    {
        ResultType.Ok => okFunc(Value!),
        ResultType.Err => errFunc(Error!),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

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
    public T Unwrap() => Type switch
    {
        ResultType.Ok => Value!,
        ResultType.Err => throw new ResultUnwrapException<T, TError>(),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    [Pure]
    public T UnwrapOr(Func<TError, T> func) => Type switch
    {
        ResultType.Ok => Value!,
        ResultType.Err => func(Error!),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

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
}