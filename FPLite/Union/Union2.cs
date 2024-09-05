using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace FPLite.Union;

public readonly record struct Union<T1, T2>(
    T1? V1 = default,
    T2? V2 = default,
    UnionType Type = UnionType.NotSet)
    where T1 : notnull where T2 : notnull
{
    [Pure]
    public static Union<T1, T2> U1([DisallowNull] T1 value) =>
        new(V1: value, Type: UnionType.T1);

    [Pure]
    public static Union<T1, T2> U2([DisallowNull] T2 value) =>
        new(V2: value, Type: UnionType.T2);

    [Pure]
    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func) => Type switch
    {
        UnionType.T1 => t1Func(V1!),
        UnionType.T2 => t2Func(V2!),
        _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
            $"{GetType()} does not support {Type.ToString()}!")
    };

    public void Match(Action<T1> t1Act, Action<T2> t2Act)
    {
        switch (Type)
        {
            case UnionType.T1:
                t1Act(V1!);
                break;
            case UnionType.T2:
                t2Act(V2!);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }
}