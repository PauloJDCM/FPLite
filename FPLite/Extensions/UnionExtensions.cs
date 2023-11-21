using FPLite.Types.Union;

namespace FPLite.Extensions;

public static class UnionExtensions
{
    /// <summary>
    /// Converts a nullable value of type T1 to a Union representing 2 possible types.
    /// </summary>
    /// <param name="t1">The nullable value of type T1 to convert.</param>
    /// <returns>
    /// Union representing 2 possible types.
    /// If the provided value is null, returns a representation of "nothing" value.
    /// Otherwise, returns a representation of the value as T1.
    /// </returns>
    public static Union<T1, T2> ToUnion<T1, T2>(this T1? t1) =>
        t1 is null ? Union<T1, T2>.Nothing : Union<T1, T2>.Type1(t1);

    /// <summary>
    /// Converts a nullable value of type T1 to a Union representing 3 possible types.
    /// </summary>
    /// <param name="t1">The nullable value of type T1 to convert.</param>
    /// <returns>
    /// Union representing 3 possible types.
    /// If the provided value is null, returns a representation of "nothing" value.
    /// Otherwise, returns a representation of the value as T1.
    /// </returns>
    public static Union<T1, T2, T3> ToUnion<T1, T2, T3>(this T1? t1) =>
        t1 is null ? Union<T1, T2, T3>.Nothing : Union<T1, T2, T3>.Type1(t1);

    /// <summary>
    /// Converts a nullable value of type T1 to a Union representing 4 possible types.
    /// </summary>
    /// <param name="t1">The nullable value of type T1 to convert.</param>
    /// <returns>
    /// Union representing 4 possible types.
    /// If the provided value is null, returns a representation of "nothing" value.
    /// Otherwise, returns a representation of the value as T1.
    /// </returns>
    public static Union<T1, T2, T3, T4> ToUnion<T1, T2, T3, T4>(this T1? t1) =>
        t1 is null ? Union<T1, T2, T3, T4>.Nothing : Union<T1, T2, T3, T4>.Type1(t1);

    /// <summary>
    /// Converts a nullable value of type T1 to a Union representing 5 possible types.
    /// </summary>
    /// <param name="t1">The nullable value of type T1 to convert.</param>
    /// <returns>
    /// Union representing 5 possible types.
    /// If the provided value is null, returns a representation of "nothing" value.
    /// Otherwise, returns a representation of the value as T1.
    /// </returns>
    public static Union<T1, T2, T3, T4, T5> ToUnion<T1, T2, T3, T4, T5>(this T1? t1) =>
        t1 is null ? Union<T1, T2, T3, T4, T5>.Nothing : Union<T1, T2, T3, T4, T5>.Type1(t1);

    /// <summary>
    /// Converts a nullable value of type T1 to a Union representing 6 possible types.
    /// </summary>
    /// <param name="t1">The nullable value of type T1 to convert.</param>
    /// <returns>
    /// Union representing 6 possible types.
    /// If the provided value is null, returns a representation of "nothing" value.
    /// Otherwise, returns a representation of the value as T1.
    /// </returns>
    public static Union<T1, T2, T3, T4, T5, T6> ToUnion<T1, T2, T3, T4, T5, T6>(this T1? t1) =>
        t1 is null ? Union<T1, T2, T3, T4, T5, T6>.Nothing : Union<T1, T2, T3, T4, T5, T6>.Type1(t1);

    /// <summary>
    /// Converts a nullable value of type T1 to a Union representing 7 possible types.
    /// </summary>
    /// <param name="t1">The nullable value of type T1 to convert.</param>
    /// <returns>
    /// Union representing 7 possible types.
    /// If the provided value is null, returns a representation of "nothing" value.
    /// Otherwise, returns a representation of the value as T1.
    /// </returns>
    public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T1? t1) =>
        t1 is null ? Union<T1, T2, T3, T4, T5, T6, T7>.Nothing : Union<T1, T2, T3, T4, T5, T6, T7>.Type1(t1);

    /// <summary>
    /// Converts a nullable value of type T1 to a Union representing 8 possible types.
    /// </summary>
    /// <param name="t1">The nullable value of type T1 to convert.</param>
    /// <returns>
    /// Union representing 8 possible types.
    /// If the provided value is null, returns a representation of "nothing" value.
    /// Otherwise, returns a representation of the value as T1.
    /// </returns>
    public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T1? t1) =>
        t1 is null ? Union<T1, T2, T3, T4, T5, T6, T7, T8>.Nothing : Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type1(t1);
}