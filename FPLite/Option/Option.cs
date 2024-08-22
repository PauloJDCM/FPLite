using System;
using FPLite.Union;

namespace FPLite.Option;

public interface IOption<T>
{
    bool IsSome { get; }

    TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc);
    void Match(Action<T> someAct, Action noneAct);

    IOption<TResult> Bind<TResult>(Func<T, TResult> someFunc);

    T Unwrap();
    T UnwrapOr(Func<T> func);
    IUnion<T, TOr> UnwrapOr<TOr>(Func<TOr> func);
}

public class OptionUnwrapException<T> : Exception
{
    private const string ErrorMessage = "Called Option<{0}>.Unwrap() on None!";

    public OptionUnwrapException() : base(string.Format(ErrorMessage, typeof(T)))
    {
    }
}

internal record Some<T>(T Value) : IOption<T>
{
    public bool IsSome => true;

    public TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> _) =>
        someFunc(Value);

    public void Match(Action<T> someAct, Action _) => someAct(Value);

    public IOption<TResult> Bind<TResult>(Func<T, TResult> someFunc) => new Some<TResult>(someFunc(Value));

    public T Unwrap() => Value;

    public T UnwrapOr(Func<T> func) => Value;

    public IUnion<T, TOr> UnwrapOr<TOr>(Func<TOr> func) => new UnionT1<T, TOr>(Value);
}

internal record None<T> : IOption<T>
{
    public bool IsSome => false;

    public TResult Match<TResult>(Func<T, TResult> _, Func<TResult> noneFunc) =>
        noneFunc();

    public void Match(Action<T> _, Action noneAct) => noneAct();

    public IOption<TResult> Bind<TResult>(Func<T, TResult> someFunc) => new None<TResult>();

    public T Unwrap() => throw new OptionUnwrapException<T>();

    public T UnwrapOr(Func<T> func) => func();

    public IUnion<T, TOr> UnwrapOr<TOr>(Func<TOr> func) => new UnionT2<T, TOr>(func());
}