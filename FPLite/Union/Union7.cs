﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace FPLite.Union;

public readonly record struct Union<T1, T2, T3, T4, T5, T6, T7>(
    T1? V1 = default,
    T2? V2 = default,
    T3? V3 = default,
    T4? V4 = default,
    T5? V5 = default,
    T6? V6 = default,
    T7? V7 = default,
    UnionType Type = UnionType.NotSet)
    where T1 : notnull
    where T2 : notnull
    where T3 : notnull
    where T4 : notnull
    where T5 : notnull
    where T6 : notnull
    where T7 : notnull
{
    /// <summary>
    /// Creates a <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/> with the given value.
    /// </summary>
    [Pure]
    public static Union<T1, T2, T3, T4, T5, T6, T7> U1([DisallowNull] T1 value) => new(V1: value, Type: UnionType.T1);

    /// <summary>
    /// Creates a <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/> with the given value.
    /// </summary>
    [Pure]
    public static Union<T1, T2, T3, T4, T5, T6, T7> U2([DisallowNull] T2 value) => new(V2: value, Type: UnionType.T2);

    /// <summary>
    /// Creates a <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/> with the given value.
    /// </summary>
    [Pure]
    public static Union<T1, T2, T3, T4, T5, T6, T7> U3([DisallowNull] T3 value) => new(V3: value, Type: UnionType.T3);

    /// <summary>
    /// Creates a <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/> with the given value.
    /// </summary>
    [Pure]
    public static Union<T1, T2, T3, T4, T5, T6, T7> U4([DisallowNull] T4 value) => new(V4: value, Type: UnionType.T4);

    /// <summary>
    /// Creates a <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/> with the given value.
    /// </summary>
    [Pure]
    public static Union<T1, T2, T3, T4, T5, T6, T7> U5([DisallowNull] T5 value) => new(V5: value, Type: UnionType.T5);

    /// <summary>
    /// Creates a <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/> with the given value.
    /// </summary>
    [Pure]
    public static Union<T1, T2, T3, T4, T5, T6, T7> U6([DisallowNull] T6 value) => new(V6: value, Type: UnionType.T6);

    /// <summary>
    /// Creates a <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/> with the given value.
    /// </summary>
    [Pure]
    public static Union<T1, T2, T3, T4, T5, T6, T7> U7([DisallowNull] T7 value) => new(V7: value, Type: UnionType.T7);

    /// <summary>
    /// Applies the appropriate function depending on the type of <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/>.
    /// </summary>
    [Pure]
    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func) =>
        Type switch
        {
            UnionType.T1 => t1Func(V1!),
            UnionType.T2 => t2Func(V2!),
            UnionType.T3 => t3Func(V3!),
            UnionType.T4 => t4Func(V4!),
            UnionType.T5 => t5Func(V5!),
            UnionType.T6 => t6Func(V6!),
            UnionType.T7 => t7Func(V7!),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    /// <summary>
    /// Applies the appropriate async function depending on the type of <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/>.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    [Pure]
    public async Task<TResult> MatchAsync<TResult>(Func<T1, CancellationToken, Task<TResult>> t1Func,
        Func<T2, CancellationToken, Task<TResult>> t2Func, Func<T3, CancellationToken, Task<TResult>> t3Func,
        Func<T4, CancellationToken, Task<TResult>> t4Func, Func<T5, CancellationToken, Task<TResult>> t5Func,
        Func<T6, CancellationToken, Task<TResult>> t6Func, Func<T7, CancellationToken, Task<TResult>> t7Func,
        CancellationToken ct = default) =>
        Type switch
        {
            UnionType.T1 => await t1Func(V1!, ct),
            UnionType.T2 => await t2Func(V2!, ct),
            UnionType.T3 => await t3Func(V3!, ct),
            UnionType.T4 => await t4Func(V4!, ct),
            UnionType.T5 => await t5Func(V5!, ct),
            UnionType.T6 => await t6Func(V6!, ct),
            UnionType.T7 => await t7Func(V7!, ct),
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type,
                $"{GetType()} does not support {Type.ToString()}!")
        };

    /// <summary>
    /// Applies the appropriate action depending on the type of <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/>.
    /// </summary>
    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act)
    {
        switch (Type)
        {
            case UnionType.T1:
                t1Act(V1!);
                break;
            case UnionType.T2:
                t2Act(V2!);
                break;
            case UnionType.T3:
                t3Act(V3!);
                break;
            case UnionType.T4:
                t4Act(V4!);
                break;
            case UnionType.T5:
                t5Act(V5!);
                break;
            case UnionType.T6:
                t6Act(V6!);
                break;
            case UnionType.T7:
                t7Act(V7!);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }

    /// <summary>
    /// Applies the appropriate async action depending on the type of <see cref="Union{T1, T2, T3, T4, T5, T6, T7}"/>.
    /// <para><b>Note:</b> The caller is responsible for using <c>ConfigureAwait</c> if necessary.</para>
    /// </summary>
    public async Task MatchAsync(Func<T1, CancellationToken, Task> t1Act,
        Func<T2, CancellationToken, Task> t2Act, Func<T3, CancellationToken, Task> t3Act,
        Func<T4, CancellationToken, Task> t4Act, Func<T5, CancellationToken, Task> t5Act,
        Func<T6, CancellationToken, Task> t6Act, Func<T7, CancellationToken, Task> t7Act,
        CancellationToken ct = default)
    {
        switch (Type)
        {
            case UnionType.T1:
                await t1Act(V1!, ct);
                break;
            case UnionType.T2:
                await t2Act(V2!, ct);
                break;
            case UnionType.T3:
                await t3Act(V3!, ct);
                break;
            case UnionType.T4:
                await t4Act(V4!, ct);
                break;
            case UnionType.T5:
                await t5Act(V5!, ct);
                break;
            case UnionType.T6:
                await t6Act(V6!, ct);
                break;
            case UnionType.T7:
                await t7Act(V7!, ct);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Type), Type,
                    $"{GetType()} does not support {Type.ToString()}!");
        }
    }
}