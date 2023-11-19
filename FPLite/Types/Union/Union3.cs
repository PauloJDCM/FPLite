namespace FPLite.Types.Union;

/// <summary>
/// Represents a discriminated union with three possible cases.
/// </summary>
public class Union3<T1, T2, T3>
{
    private readonly T1? _t1;
    private readonly T2? _t2;
    private readonly T3? _t3;

    private Union3()
    {
    }

    private Union3(T1 t1)
    {
        _t1 = t1;
    }

    private Union3(T2 t2)
    {
        _t2 = t2;
    }

    private Union3(T3 t3)
    {
        _t3 = t3;
    }

    /// <summary>
    /// Represents a Union of 3 types with no value. Used to indicate the absence of a value in Union types.
    /// </summary>
    public static Union3<T1, T2, T3> Nothing => new();

    /// <summary>
    /// Creates a Union with a value of Type 1, or returns Nothing if the provided value is null.
    /// </summary>
    public static Union3<T1, T2, T3> Type1(T1? t1) => t1 is not null ? new(t1) : Nothing;

    /// <summary>
    /// Creates a Union with a value of Type 2, or returns Nothing if the provided value is null.
    /// </summary>
    public static Union3<T1, T2, T3> Type2(T2? t2) => t2 is not null ? new(t2) : Nothing;

    /// <summary>
    /// Creates a Union with a value of Type 3, or returns Nothing if the provided value is null.
    /// </summary>
    public static Union3<T1, T2, T3> Type3(T3? t3) => t3 is not null ? new(t3) : Nothing;

    /// <summary>
    /// Matches the active case and invokes the appropriate delegate.
    /// </summary>
    public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2, Func<T3, TResult> case3, Func<TResult> caseNothing)
    {
        if (_t1 is not null) return case1(_t1);
        if (_t2 is not null) return case2(_t2);
        if (_t3 is not null) return case3(_t3);
        return caseNothing();
    }

    /// <summary>
    /// Matches the active case and invokes the appropriate action.
    /// </summary>
    public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action caseNothing)
    {
        if (_t1 is not null) case1(_t1);
        else if (_t2 is not null) case2(_t2);
        else if (_t3 is not null) case3(_t3);
        else caseNothing();
    }
}
