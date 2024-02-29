using FPLite.Union;

namespace FPLite.Extensions
{
    public static class UnionExtensions
    {
        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 2 possible types.
        /// </summary>
        public static Union<T1, T2> ToUnion<T1, T2>(this T1 t1) => Union<T1, T2>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T2 to a Union representing 2 possible types.
        /// </summary>
        public static Union<T1, T2> ToUnion<T1, T2>(this T2 t2) => Union<T1, T2>.Type2(t2);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 3 possible types.
        /// </summary>
        public static Union<T1, T2, T3> ToUnion<T1, T2, T3>(this T1 t1) =>
            Union<T1, T2, T3>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T2 to a Union representing 3 possible types.
        /// </summary>
        public static Union<T1, T2, T3> ToUnion<T1, T2, T3>(this T2 t2) =>
            Union<T1, T2, T3>.Type2(t2);

        /// <summary>
        /// Converts a nullable value of type T3 to a Union representing 3 possible types.
        /// </summary>
        public static Union<T1, T2, T3> ToUnion<T1, T2, T3>(this T3 t3) =>
            Union<T1, T2, T3>.Type3(t3);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 4 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4> ToUnion<T1, T2, T3, T4>(this T1 t1) =>
            Union<T1, T2, T3, T4>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T2 to a Union representing 4 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4> ToUnion<T1, T2, T3, T4>(this T2 t2) =>
            Union<T1, T2, T3, T4>.Type2(t2);

        /// <summary>
        /// Converts a nullable value of type T3 to a Union representing 4 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4> ToUnion<T1, T2, T3, T4>(this T3 t3) =>
            Union<T1, T2, T3, T4>.Type3(t3);

        /// <summary>
        /// Converts a nullable value of type T4 to a Union representing 4 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4> ToUnion<T1, T2, T3, T4>(this T4 t4) =>
            Union<T1, T2, T3, T4>.Type4(t4);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 5 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5> ToUnion<T1, T2, T3, T4, T5>(this T1 t1) =>
            Union<T1, T2, T3, T4, T5>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T2 to a Union representing 5 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5> ToUnion<T1, T2, T3, T4, T5>(this T2 t2) =>
            Union<T1, T2, T3, T4, T5>.Type2(t2);

        /// <summary>
        /// Converts a nullable value of type T3 to a Union representing 5 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5> ToUnion<T1, T2, T3, T4, T5>(this T3 t3) =>
            Union<T1, T2, T3, T4, T5>.Type3(t3);

        /// <summary>
        /// Converts a nullable value of type T4 to a Union representing 5 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5> ToUnion<T1, T2, T3, T4, T5>(this T4 t4) =>
            Union<T1, T2, T3, T4, T5>.Type4(t4);

        /// <summary>
        /// Converts a nullable value of type T5 to a Union representing 5 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5> ToUnion<T1, T2, T3, T4, T5>(this T5 t5) =>
            Union<T1, T2, T3, T4, T5>.Type5(t5);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 6 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6> ToUnion<T1, T2, T3, T4, T5, T6>(this T1 t1) =>
            Union<T1, T2, T3, T4, T5, T6>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T2 to a Union representing 6 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6> ToUnion<T1, T2, T3, T4, T5, T6>(this T2 t2) =>
            Union<T1, T2, T3, T4, T5, T6>.Type2(t2);

        /// <summary>
        /// Converts a nullable value of type T3 to a Union representing 6 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6> ToUnion<T1, T2, T3, T4, T5, T6>(this T3 t3) =>
            Union<T1, T2, T3, T4, T5, T6>.Type3(t3);

        /// <summary>
        /// Converts a nullable value of type T4 to a Union representing 6 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6> ToUnion<T1, T2, T3, T4, T5, T6>(this T4 t4) =>
            Union<T1, T2, T3, T4, T5, T6>.Type4(t4);

        /// <summary>
        /// Converts a nullable value of type T5 to a Union representing 6 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6> ToUnion<T1, T2, T3, T4, T5, T6>(this T5 t5) =>
            Union<T1, T2, T3, T4, T5, T6>.Type5(t5);

        /// <summary>
        /// Converts a nullable value of type T6 to a Union representing 6 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6> ToUnion<T1, T2, T3, T4, T5, T6>(this T6 t6) =>
            Union<T1, T2, T3, T4, T5, T6>.Type6(t6);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 7 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T1 t1) =>
            Union<T1, T2, T3, T4, T5, T6, T7>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T2 to a Union representing 7 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T2 t2) =>
            Union<T1, T2, T3, T4, T5, T6, T7>.Type2(t2);

        /// <summary>
        /// Converts a nullable value of type T3 to a Union representing 7 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T3 t3) =>
            Union<T1, T2, T3, T4, T5, T6, T7>.Type3(t3);

        /// <summary>
        /// Converts a nullable value of type T4 to a Union representing 7 possible types.
        /// </summary>
        /// <returns></returns>
        public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T4 t4) =>
            Union<T1, T2, T3, T4, T5, T6, T7>.Type4(t4);

        /// <summary>
        /// Converts a nullable value of type T5 to a Union representing 7 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T5 t5) =>
            Union<T1, T2, T3, T4, T5, T6, T7>.Type5(t5);

        /// <summary>
        /// Converts a nullable value of type T6 to a Union representing 7 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T6 t6) =>
            Union<T1, T2, T3, T4, T5, T6, T7>.Type6(t6);

        /// <summary>
        /// Converts a nullable value of type T7 to a Union representing 7 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7> ToUnion<T1, T2, T3, T4, T5, T6, T7>(this T7 t7) =>
            Union<T1, T2, T3, T4, T5, T6, T7>.Type7(t7);

        /// <summary>
        /// Converts a nullable value of type T1 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T1 t1) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type1(t1);

        /// <summary>
        /// Converts a nullable value of type T2 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T2 t2) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type2(t2);

        /// <summary>
        /// Converts a nullable value of type T3 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T3 t3) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type3(t3);

        /// <summary>
        /// Converts a nullable value of type T4 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T4 t4) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type4(t4);

        /// <summary>
        /// Converts a nullable value of type T5 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T5 t5) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type5(t5);

        /// <summary>
        /// Converts a nullable value of type T6 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T6 t6) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type6(t6);

        /// <summary>
        /// Converts a nullable value of type T7 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T7 t7) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type7(t7);

        /// <summary>
        /// Converts a nullable value of type T8 to a Union representing 8 possible types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7, T8> ToUnion<T1, T2, T3, T4, T5, T6, T7, T8>(this T8 t8) =>
            Union<T1, T2, T3, T4, T5, T6, T7, T8>.Type8(t8);
    }
}