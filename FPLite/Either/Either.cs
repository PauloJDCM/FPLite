using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace FPLite.Either;

public enum EitherType : byte
{
    NotSet,
    Neither,
    Left,
    Right,
    Both
}

public readonly record struct Either<TLeft, TRight>(
    TLeft? L = default,
    TRight? R = default,
    EitherType Type = EitherType.NotSet)
    where TLeft : notnull where TRight : notnull
{
    [Pure]
    public static Either<TLeft, TRight> Left([DisallowNull] TLeft value) => new(L: value, Type: EitherType.Left);

    [Pure]
    public static Either<TLeft, TRight> Right([DisallowNull] TRight value) => new(R: value, Type: EitherType.Right);

    [Pure]
    public static Either<TLeft, TRight> Both([DisallowNull] TLeft left, [DisallowNull] TRight right) =>
        new(L: left, R: right, Type: EitherType.Both);

    [Pure]
    public static Either<TLeft, TRight> Neither() => new(Type: EitherType.Neither);

    [Pure]
    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
        Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc) => Type switch
    {
        EitherType.Neither => neitherFunc(),
        EitherType.Both => bothFunc(L!, R!),
        EitherType.Left => leftFunc(L!),
        EitherType.Right => rightFunc(R!),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    public void Match(Action<TLeft> leftAct, Action<TRight> rightAct, Action neitherAct,
        Action<TLeft, TRight> bothAct)
    {
        switch (Type)
        {
            case EitherType.Neither:
                neitherAct();
                break;
            case EitherType.Both:
                bothAct(L!, R!);
                break;
            case EitherType.Left:
                leftAct(L!);
                break;
            case EitherType.Right:
                rightAct(R!);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }
}