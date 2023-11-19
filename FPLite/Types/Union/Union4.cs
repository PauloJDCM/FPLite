﻿namespace FPLite.Types.Union;

/// <summary>
/// Represents a discriminated union with four possible cases.
/// </summary>
public class Union4<T1, T2, T3, T4>
{
    private readonly T1? _t1;
    private readonly T2? _t2;
    private readonly T3? _t3;
    private readonly T4? _t4;

    private Union4()
    {
    }

    private Union4(T1 t1)
    {
        _t1 = t1;
    }

    private Union4(T2 t2)
    {
        _t2 = t2;
    }

    private Union4(T3 t3)
    {
        _t3 = t3;
    }

    private Union4(T4 t4)
    {
        _t4 = t4;
    }

    /// <summary>
    /// Represents a Union of 4 types with no value. Used to indicate the absence of a value in Union types.
    /// </summary>
    public static Union4<T1, T2, T3, T4> Nothing => new();

    /// <summary>
    /// Creates a Union with a value of Type 1, or returns Nothing if the provided value is null.
    /// </summary>
    public static Union4<T1, T2, T3, T4> Type1(T1? t1) => t1 is not null ? new(t1) : Nothing;

    /// <summary>
    /// Creates a Union with a value of Type 2, or returns Nothing if the provided value is null.
    /// </summary>
    public static Union4<T1, T2, T3, T4> Type2(T2? t2) => t2 is not null ? new(t2) : Nothing;

    /// <summary>
    /// Creates a Union with a value of Type 3, or returns Nothing if the provided value is null.
    /// </summary>
    public static Union4<T1, T2, T3, T4> Type3(T3? t3) => t3 is not null ? new(t3) : Nothing;

    /// <summary>
    /// Creates a Union with a value of Type 4, or returns Nothing if the provided value is null.
    /// </summary>
    public static Union4<T1, T2, T3, T4> Type4(T4? t4) => t4 is not null ? new(t4) : Nothing;

    /// <summary>
    /// Matches the active case and invokes the appropriate delegate.
    /// </summary>
    public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2, Func<T3, TResult> case3,
        Func<T4, TResult> case4, Func<TResult> caseNothing)
    {
        if (_t1 is not null) return case1(_t1);
        if (_t2 is not null) return case2(_t2);
        if (_t3 is not null) return case3(_t3);
        if (_t4 is not null) return case4(_t4);
        return caseNothing();
    }

    /// <summary>
    /// Matches the active case and invokes the appropriate action.
    /// </summary>
    public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action<T4> case4, Action caseNothing)
    {
        if (_t1 is not null) case1(_t1);
        else if (_t2 is not null) case2(_t2);
        else if (_t3 is not null) case3(_t3);
        else if (_t4 is not null) case4(_t4);
        else caseNothing();
    }
}