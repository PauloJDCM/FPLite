using System;
using FPLite.Union;

namespace FPLite.Result;

internal sealed class ResultUnwrapException<T, TError> : UnwrapException
{
    private const string ErrorMessage = "Called Result<{0}, {1}>.Unwrap() on Error!";

    public ResultUnwrapException() : base(string.Format(ErrorMessage, typeof(T), typeof(TError)))
    {
    }
}

internal record Ok<T, TError>(T Value) : IResult<T, TError>
{
    public bool IsOk => true;

    public TResult Match<TResult>(Func<T, TResult> okFunc, Func<TError, TResult> _) => okFunc(Value);

    public void Match(Action<T> okAct, Action<TError> _) => okAct(Value);

    public IResult<TResult, TError> Bind<TResult>(Func<T, TResult> okFunc) => new Ok<TResult, TError>(okFunc(Value));

    public T Unwrap() => Value;

    public T UnwrapOr(Func<TError, T> func) => Value;

    public IUnion<T, TOr> UnwrapOr<TOr>(Func<TError, TOr> func) => new UnionT1<T, TOr>(Value);
}

internal record Err<T, TError>(TError Value) : IResult<T, TError>
{
    public bool IsOk => false;

    public TResult Match<TResult>(Func<T, TResult> _, Func<TError, TResult> errFunc) => errFunc(Value);

    public void Match(Action<T> _, Action<TError> errAct) => errAct(Value);

    public IResult<TResult, TError> Bind<TResult>(Func<T, TResult> _) => new Err<TResult, TError>(Value);

    public T Unwrap() => throw new ResultUnwrapException<T, TError>();

    public T UnwrapOr(Func<TError, T> func) => func(Value);

    public IUnion<T, TOr> UnwrapOr<TOr>(Func<TError, TOr> func) => new UnionT2<T, TOr>(func(Value));
}