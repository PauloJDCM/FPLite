using System;

namespace FPLite.Union;

internal record UnionT1<T1, T2, T3, T4>(T1 Value) : IUnion<T1, T2, T3, T4>
{
    public UnionType Type => UnionType.T1;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func) => t1Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act) => t1Act(Value);
}

internal record UnionT2<T1, T2, T3, T4>(T2 Value) : IUnion<T1, T2, T3, T4>
{
    public UnionType Type => UnionType.T2;


    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func) => t2Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act) => t2Act(Value);
}

internal record UnionT3<T1, T2, T3, T4>(T3 Value) : IUnion<T1, T2, T3, T4>
{
    public UnionType Type => UnionType.T3;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func) => t3Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act) => t3Act(Value);
}

internal record UnionT4<T1, T2, T3, T4>(T4 Value) : IUnion<T1, T2, T3, T4>
{
    public UnionType Type => UnionType.T4;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func,
        Func<T4, TResult> t4Func) => t4Func(Value);

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act, Action<T4> t4Act) => t4Act(Value);
}