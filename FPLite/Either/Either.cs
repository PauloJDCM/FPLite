using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

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
    /// Applies the appropriate async function depending on the type of <see cref="Either{TLeft, TRight}" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    [Pure]
    public async Task<TResult> MatchAsync<TResult>(
        Func<TLeft, CancellationToken, Task<TResult>> leftFunc,
        Func<TRight, CancellationToken, Task<TResult>> rightFunc,
        Func<CancellationToken, Task<TResult>> neitherFunc,
        Func<TLeft, TRight, CancellationToken, Task<TResult>> bothFunc,
        CancellationToken ct = default)
        where TResult : notnull => Type switch
    {
        EitherType.Neither => await neitherFunc(ct),
        EitherType.Both => await bothFunc(L!, R!, ct),
        EitherType.Left => await leftFunc(L!, ct),
        EitherType.Right => await rightFunc(R!, ct),
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

    /// <summary>
    /// Applies the appropriate async action depending on the type of <see cref="Either{TLeft, TRight}" />.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    public async Task MatchAsync(Func<TLeft, CancellationToken, Task> leftAct,
        Func<TRight, CancellationToken, Task> rightAct, Func<CancellationToken, Task> neitherAct,
        Func<TLeft, TRight, CancellationToken, Task> bothAct, CancellationToken ct = default)
    {
        switch (Type)
        {
            case EitherType.Neither:
                await neitherAct(ct);
                break;
            case EitherType.Both:
                await bothAct(L!, R!, ct);
                break;
            case EitherType.Left:
                await leftAct(L!, ct);
                break;
            case EitherType.Right:
                await rightAct(R!, ct);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }
}