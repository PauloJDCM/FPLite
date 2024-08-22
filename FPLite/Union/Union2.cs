using System;

namespace FPLite.Union;

public interface IUnion<out T1, out T2>
{
    UnionType Type { get; }

    TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func);
    IUnion<T1Result, T2Result> Match<TResult, T1Result, T2Result>(Func<T1, T1Result> t1Func, Func<T2, T2Result> t2Func);
    void Match(Action<T1> t1Act, Action<T2> t2Act);
}

internal record UnionT1<T1, T2>(T1 Value) : IUnion<T1, T2>
{
    public UnionType Type => UnionType.T1;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func) => t1Func(Value);

    public IUnion<T1Result, T2Result> Match<TResult, T1Result, T2Result>(Func<T1, T1Result> t1Func,
        Func<T2, T2Result> t2Func) => new UnionT1<T1Result, T2Result>(t1Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act) => t1Act(Value);
}

internal record UnionT2<T1, T2>(T2 Value) : IUnion<T1, T2>
{
    public UnionType Type => UnionType.T2;

    public TResult Match<TResult>(Func<T1, TResult> t1Func, Func<T2, TResult> t2Func) => t2Func(Value);

    public IUnion<T1Result, T2Result> Match<TResult, T1Result, T2Result>(Func<T1, T1Result> t1Func,
        Func<T2, T2Result> t2Func) => new UnionT2<T1Result, T2Result>(t2Func(Value));

    public void Match(Action<T1> t1Act, Action<T2> t2Act) => t2Act(Value);
}