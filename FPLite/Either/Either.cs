using System;

namespace FPLite.Either;

public enum EitherType : byte
{
    Neither,
    Left,
    Right,
    Both
}

public interface IEither<out TLeft, out TRight>
{
    EitherType Type { get; }

    TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
        Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc);

    IEither<TResultLeft, TResultRight> Match<TResultLeft, TResultRight>(Func<TLeft, TResultLeft> leftFunc,
        Func<TRight, TResultRight> rightFunc, Func<IEither<TResultLeft, TResultRight>> neitherFunc,
        Func<TLeft, TRight, IEither<TResultLeft, TResultRight>> bothFunc);

    void Match(Action<TLeft> leftAct, Action<TRight> rightAct, Action neitherAct, Action<TLeft, TRight> bothAct);
}

internal record Left<TLeft, TRight>(TLeft Value) : IEither<TLeft, TRight>
{
    public EitherType Type => EitherType.Left;

    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
        Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc) => leftFunc(Value);

    public IEither<TResultLeft, TResultRight> Match<TResultLeft, TResultRight>(Func<TLeft, TResultLeft> leftFunc,
        Func<TRight, TResultRight> rightFunc, Func<IEither<TResultLeft, TResultRight>> neitherFunc,
        Func<TLeft, TRight, IEither<TResultLeft, TResultRight>> bothFunc) =>
        new Left<TResultLeft, TResultRight>(leftFunc(Value));

    public void Match(Action<TLeft> leftAct, Action<TRight> rightAct, Action neitherAct,
        Action<TLeft, TRight> bothAct) => leftAct(Value);
}

internal record Right<TLeft, TRight>(TRight Value) : IEither<TLeft, TRight>
{
    public EitherType Type => EitherType.Right;

    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
        Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc) => rightFunc(Value);

    public IEither<TResultLeft, TResultRight> Match<TResultLeft, TResultRight>(Func<TLeft, TResultLeft> leftFunc,
        Func<TRight, TResultRight> rightFunc, Func<IEither<TResultLeft, TResultRight>> neitherFunc,
        Func<TLeft, TRight, IEither<TResultLeft, TResultRight>> bothFunc) =>
        new Right<TResultLeft, TResultRight>(rightFunc(Value));

    public void Match(Action<TLeft> leftAct, Action<TRight> rightAct, Action neitherAct,
        Action<TLeft, TRight> bothAct) => rightAct(Value);
}

internal record Neither<TLeft, TRight> : IEither<TLeft, TRight>
{
    public EitherType Type => EitherType.Neither;

    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
        Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc) => neitherFunc();

    public IEither<TResultLeft, TResultRight> Match<TResultLeft, TResultRight>(Func<TLeft, TResultLeft> leftFunc,
        Func<TRight, TResultRight> rightFunc, Func<IEither<TResultLeft, TResultRight>> neitherFunc,
        Func<TLeft, TRight, IEither<TResultLeft, TResultRight>> bothFunc) =>
        neitherFunc();

    public void Match(Action<TLeft> leftAct, Action<TRight> rightAct, Action neitherAct,
        Action<TLeft, TRight> bothAct) => neitherAct();
}

internal record Both<TLeft, TRight>(TLeft Left, TRight Right) : IEither<TLeft, TRight>
{
    public EitherType Type => EitherType.Both;

    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
        Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc) => bothFunc(Left, Right);

    public IEither<TResultLeft, TResultRight> Match<TResultLeft, TResultRight>(Func<TLeft, TResultLeft> leftFunc,
        Func<TRight, TResultRight> rightFunc, Func<IEither<TResultLeft, TResultRight>> neitherFunc,
        Func<TLeft, TRight, IEither<TResultLeft, TResultRight>> bothFunc) =>
        new Both<TResultLeft, TResultRight>(leftFunc(Left), rightFunc(Right));

    public void Match(Action<TLeft> leftAct, Action<TRight> rightAct, Action neitherAct,
        Action<TLeft, TRight> bothAct) => bothAct(Left, Right);
}