namespace FPLite.Types.Union;

/// <summary>
/// Represents a discriminated union with two possible cases.
/// </summary>
public class Union2<T1, T2>
{
    private readonly T1? _t1;
    private readonly T2? _t2;

    private Union2()
    {
    }

    private Union2(T1 t1)
    {
        _t1 = t1;
    }

    private Union2(T2 t2)
    {
        _t2 = t2;
    }

    /// <summary>
    /// Represents a Union of 2 types with no value. Used to indicate the absence of a value in Union types.
    /// </summary>
    public static Union2<T1, T2> Nothing => new();

    /// <summary>
    /// Creates a Union with a value of Type 1, or returns Nothing if the provided value is null.
    /// </summary>
    /// <param name="t1">The value of Type 1 to be included in the Union, or null.</param>
    /// <returns>
    /// A Union containing the provided value if it is not null, or Nothing if the value is null.
    /// </returns>
    public static Union2<T1, T2> Type1(T1? t1) => t1 is not null ? new(t1) : Nothing;

    /// <summary>
    /// Creates a Union with a value of Type 2, or returns Nothing if the provided value is null.
    /// </summary>
    /// <param name="t2">The value of Type 2 to be included in the Union, or null.</param>
    /// <returns>
    /// A Union containing the provided value if it is not null, or Nothing if the value is null.
    /// </returns>
    public static Union2<T1, T2> Type2(T2? t2) => t2 is not null ? new(t2) : Nothing;

    /// <summary>
    /// Matches the active case and invokes the appropriate delegate.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <returns>The result of the invoked delegate.</returns>
    public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2, Func<TResult> caseNothing)
    {
        if (_t1 is not null) return case1(_t1);
        if (_t2 is not null) return case2(_t2);
        return caseNothing();
    }

    /// <summary>
    /// Matches the active case and invokes the appropriate action.
    /// </summary>
    public void Match(Action<T1> case1, Action<T2> case2, Action caseNothing)
    {
        if (_t1 is not null) case1(_t1);
        else if (_t2 is not null) case2(_t2);
        else caseNothing();
    }

    /// <summary>
    /// Matches the active case and invokes the appropriate delegate.
    /// </summary>
    /// <returns>The result of the invoked delegate or Nothing.</returns>
    public Union2<TResult1, TResult2> Match<TResult1, TResult2>(Func<T1, TResult1> case1, Func<T2, TResult2> case2)
    {
        if (_t1 is not null) return Union2<TResult1, TResult2>.Type1(case1(_t1));
        if (_t2 is not null) return Union2<TResult1, TResult2>.Type2(case2(_t2));
        return Union2<TResult1, TResult2>.Nothing;
    }
    
    /// <summary>
    /// Gets the value contained in the union.
    /// </summary>
    /// <returns>
    /// The value contained in the union, which can be of any type specified by the generic parameters.
    /// Returns null if all values are null.
    /// </returns>
    public object? GetValue()
    {
        if (_t1 is not null) return _t1;
        if (_t2 is not null) return _t2;
        return null;
    }
}