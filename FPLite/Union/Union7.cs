using System;

namespace FPLite.Union;

internal record UnionT1<T1, T2, T3, T4, T5, T6, T7>(T1 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7>
{
    public UnionType Type => UnionType.T1;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func) =>
        t1Func(Value);

    public IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result> Match<T1Result, T2Result,
        T3Result, T4Result, T5Result, T6Result, T7Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func,
        Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func,
        Func<T7, T7Result> t7Func) =>
        new UnionT1<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result>(t1Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act) => t1Act(Value);
}

internal record UnionT2<T1, T2, T3, T4, T5, T6, T7>(T2 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7>
{
    public UnionType Type => UnionType.T2;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func) =>
        t2Func(Value);

    public IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result> Match<T1Result, T2Result,
        T3Result, T4Result, T5Result, T6Result, T7Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func,
        Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func,
        Func<T7, T7Result> t7Func) =>
        new UnionT2<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result>(t2Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act) => t2Act(Value);
}

internal record UnionT3<T1, T2, T3, T4, T5, T6, T7>(T3 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7>
{
    public UnionType Type => UnionType.T3;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func) =>
        t3Func(Value);

    public IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result> Match<T1Result, T2Result,
        T3Result, T4Result, T5Result, T6Result, T7Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func,
        Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func,
        Func<T7, T7Result> t7Func) =>
        new UnionT3<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result>(t3Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act) => t3Act(Value);
}

internal record UnionT4<T1, T2, T3, T4, T5, T6, T7>(T4 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7>
{
    public UnionType Type => UnionType.T4;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func) =>
        t4Func(Value);

    public IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result> Match<T1Result, T2Result,
        T3Result, T4Result, T5Result, T6Result, T7Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func,
        Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func,
        Func<T7, T7Result> t7Func) =>
        new UnionT4<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result>(t4Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act) => t4Act(Value);
}

internal record UnionT5<T1, T2, T3, T4, T5, T6, T7>(T5 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7>
{
    public UnionType Type => UnionType.T5;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func) =>
        t5Func(Value);

    public IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result> Match<T1Result, T2Result,
        T3Result, T4Result, T5Result, T6Result, T7Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func,
        Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func,
        Func<T7, T7Result> t7Func) =>
        new UnionT5<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result>(t5Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act) => t5Act(Value);
}

internal record UnionT6<T1, T2, T3, T4, T5, T6, T7>(T6 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7>
{
    public UnionType Type => UnionType.T6;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func) =>
        t6Func(Value);

    public IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result> Match<T1Result, T2Result,
        T3Result, T4Result, T5Result, T6Result, T7Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func,
        Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func,
        Func<T7, T7Result> t7Func) =>
        new UnionT6<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result>(t6Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act) => t6Act(Value);
}

internal record UnionT7<T1, T2, T3, T4, T5, T6, T7>(T7 Value) : IUnion<T1, T2, T3, T4, T5, T6, T7>
{
    public UnionType Type => UnionType.T7;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func, Func<T5, TResult> t5Func, Func<T6, TResult> t6Func, Func<T7, TResult> t7Func) =>
        t7Func(Value);

    public IUnion<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result> Match<T1Result, T2Result,
        T3Result, T4Result, T5Result, T6Result, T7Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func,
        Func<T3, T3Result> t3Func, Func<T4, T4Result> t4Func, Func<T5, T5Result> t5Func, Func<T6, T6Result> t6Func,
        Func<T7, T7Result> t7Func) =>
        new UnionT7<T1Result, T2Result, T3Result, T4Result, T5Result, T6Result, T7Result>(t7Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act, Action<T5> t5Act,
        Action<T6> t6Act, Action<T7> t7Act) => t7Act(Value);
}