using FPLite.Union;

namespace FPLite.Extensions
{
    public static class UnionExtensions
    {
        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 2 possible types.
        /// </summary>
        public static Union<T1, T2> ToUnion<T1, T2>(this T1 t1) =>
            Union<T1, T2>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 3 possible types.
        /// </summary>
        public static Union<T1, T2, T3> ToUnion<T1, T2, T3>(this T1 t1) =>
            Union<T1, T2, T3>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 4 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4> ToUnion<T1, T2, T3, T4>(this T1 t1) =>
            Union<T1, T2, T3, T4>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 5 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5> ToUnion<T1, T2, T3, T4, T5>(this T1 t1) =>
            Union<T1, T2, T3, T4, T5>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 6 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6> ToUnion<T1, T2, T3, T4, T5, T6>(this T1 t1) =>
            Union<T1, T2, T3, T4, T5, T6>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 7 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T1 t1) =>
            Union<T1, T2, T3, T4, T5, T6, T7>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T1 t1) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type1(t1);
    }
}