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
    /// <summary>
    /// Creates a <see cref="Either{TLeft, TRight}" /> with <paramref name="value" /> as the Left.
    /// </summary>
    [Pure]
    public static Either<TLeft, TRight> Left([DisallowNull] TLeft value) => new(L: value, Type: EitherType.Left);

    /// <summary>
    /// Creates a <see cref="Either{TLeft, TRight}" /> with <paramref name="value" /> as the Right.
    /// </summary>
    [Pure]
    public static Either<TLeft, TRight> Right([DisallowNull] TRight value) => new(R: value, Type: EitherType.Right);

    /// <summary>
    /// Creates a <see cref="Either{TLeft, TRight}" /> with <paramref name="left" /> and <paramref name="right" />.
    /// </summary>
    [Pure]
    public static Either<TLeft, TRight> Both([DisallowNull] TLeft left, [DisallowNull] TRight right) =>
        new(L: left, R: right, Type: EitherType.Both);

    /// <summary>
    /// Creates a <see cref="Either{TLeft, TRight}" /> with no value.
    /// </summary>
    [Pure]
    public static Either<TLeft, TRight> Neither() => new(Type: EitherType.Neither);

    /// <summary>
    /// Applies the appropriate function depending on the type of <see cref="Either{TLeft, TRight}" />.
    /// </summary>
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

    
    /// <summary>
    /// Applies the appropriate action depending on the type of <see cref="Either{TLeft, TRight}" />.
    /// </summary>
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