using System;

namespace FPLite.Union;

public interface IUnion<out T1, out T2, out T3>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func);

    IUnion<T1Result, T2Result, T3Result> Match<TResult, T1Result, T2Result, T3Result>(Func<T1, T1Result> t1Func,
        Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func);

    void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act);
}

internal record UnionT1<T1, T2, T3>(T1 Value) : IUnion<T1, T2, T3>
{
    public UnionType Type => UnionType.T1;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func) =>
        t1Func(Value);

    public IUnion<T1Result, T2Result, T3Result> Match<TResult, T1Result, T2Result, T3Result>(Func<T1, T1Result> t1Func,
        Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func) =>
        new UnionT1<T1Result, T2Result, T3Result>(t1Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act) => t1Act(Value);
}

internal record UnionT2<T1, T2, T3>(T2 Value) : IUnion<T1, T2, T3>
{
    public UnionType Type => UnionType.T2;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func) =>
        t2Func(Value);

    public IUnion<T1Result, T2Result, T3Result> Match<TResult, T1Result, T2Result, T3Result>(Func<T1, T1Result> t1Func,
        Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func) =>
        new UnionT2<T1Result, T2Result, T3Result>(t2Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act) => t2Act(Value);
}

internal record UnionT3<T1, T2, T3>(T3 Value) : IUnion<T1, T2, T3>
{
    public UnionType Type => UnionType.T3;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func, Func<T3, TResult> t3Func) =>
        t3Func(Value);

    public IUnion<T1Result, T2Result, T3Result> Match<TResult, T1Result, T2Result, T3Result>(Func<T1, T1Result> t1Func,
        Func<T2, T2Result> t2Func, Func<T3, T3Result> t3Func) =>
        new UnionT3<T1Result, T2Result, T3Result>(t3Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act, Action<T3> t3Act) => t3Act(Value);
}