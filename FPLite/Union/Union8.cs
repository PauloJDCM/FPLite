﻿using System;

namespace FPLite.Union;

internal record UnionT1<T1, T2, T3, T4, T5, T6, T7, T8>(T1 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public UnionType Type => UnionType.T1;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func) => t1Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act) => t1Act(Value);
}

internal record UnionT2<T1, T2, T3, T4, T5, T6, T7, T8>(T2 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public UnionType Type => UnionType.T2;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func) => t2Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act) => t2Act(Value);
}

internal record UnionT3<T1, T2, T3, T4, T5, T6, T7, T8>(T3 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public UnionType Type => UnionType.T3;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func) => t3Func(Value);


    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act) => t3Act(Value);
}

internal record UnionT4<T1, T2, T3, T4, T5, T6, T7, T8>(T4 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public UnionType Type => UnionType.T4;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func) => t4Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act) => t4Act(Value);
}

internal record UnionT5<T1, T2, T3, T4, T5, T6, T7, T8>(T5 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public UnionType Type => UnionType.T5;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func) => t5Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act) => t5Act(Value);
}

internal record UnionT6<T1, T2, T3, T4, T5, T6, T7, T8>(T6 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public UnionType Type => UnionType.T6;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func) => t6Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act) => t6Act(Value);
}

internal record UnionT7<T1, T2, T3, T4, T5, T6, T7, T8>(T7 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public UnionType Type => UnionType.T7;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func) => t7Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act) => t7Act(Value);
}

internal record UnionT8<T1, T2, T3, T4, T5, T6, T7, T8>(T8 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public UnionType Type => UnionType.T8;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func,
        Func<T8, TResult> t8Func) => t8Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act, Action<T8> t8Act) => t8Act(Value);
}