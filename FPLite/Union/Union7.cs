using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace FPLite.Union
{
    /// <summary>
    /// Represents a discriminated union with two possible cases.
    /// </summary>
    public class Union<T1, T2, T3, T4, T5, T6, T7>
    {
        private readonly byte _type;
        private readonly T1 _t1;
        private readonly T2 _t2;
        private readonly T3 _t3;
        private readonly T4 _t4;
        private readonly T5 _t5;
        private readonly T6 _t6;
        private readonly T7 _t7;

        private Union()
        {
        }

        private Union(T1 t1)
        {
            _type = 1;
            _t1 = t1;
        }

        private Union(T2 t2)
        {
            _type = 2;
            _t2 = t2;
        }

        private Union(T3 t3)
        {
            _type = 3;
            _t3 = t3;
        }

        private Union(T4 t4)
        {
            _type = 4;
            _t4 = t4;
        }

        private Union(T5 t5)
        {
            _type = 5;
            _t5 = t5;
        }

        private Union(T6 t6)
        {
            _type = 6;
            _t6 = t6;
        }

        private Union(T7 t7)
        {
            _type = 7;
            _t7 = t7;
        }

        /// <summary>
        /// Represents a Union of 2 types with no value. Used to indicate the absence of a value in Union types.
        /// </summary>
        public static Union<T1, T2, T3, T4, T5, T6, T7> Nothing => new Union<T1, T2, T3, T4, T5, T6, T7>();

        /// <summary>
        /// Creates a Union with a value of Type 1, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t1">The value of Type 1 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4, T5, T6, T7> Type1(T1 t1) =>
            t1 is null ? Nothing : new Union<T1, T2, T3, T4, T5, T6, T7>(t1);

        /// <summary>
        /// Creates a Union with a value of Type 2, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t2">The value of Type 2 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4, T5, T6, T7> Type2(T2 t2) =>
            t2 is null ? Nothing : new Union<T1, T2, T3, T4, T5, T6, T7>(t2);

        /// <summary>
        /// Creates a Union with a value of Type 3, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t3">The value of Type 3 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4, T5, T6, T7> Type3(T3 t3) =>
            t3 is null ? Nothing : new Union<T1, T2, T3, T4, T5, T6, T7>(t3);

        /// <summary>
        /// Creates a Union with a value of Type 4, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t4">The value of Type 4 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4, T5, T6, T7> Type4(T4 t4) =>
            t4 is null ? Nothing : new Union<T1, T2, T3, T4, T5, T6, T7>(t4);

        /// <summary>
        /// Creates a Union with a value of Type 5, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t5">The value of Type 5 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4, T5, T6, T7> Type5(T5 t5) =>
            t5 is null ? Nothing : new Union<T1, T2, T3, T4, T5, T6, T7>(t5);

        /// <summary>
        /// Creates a Union with a value of Type 6, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t6">The value of Type 6 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4, T5, T6, T7> Type6(T6 t6) =>
            t6 is null ? Nothing : new Union<T1, T2, T3, T4, T5, T6, T7>(t6);

        /// <summary>
        /// Creates a Union with a value of Type 7, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t7">The value of Type 7 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4, T5, T6, T7> Type7(T7 t7) =>
            t7 is null ? Nothing : new Union<T1, T2, T3, T4, T5, T6, T7>(t7);

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action<T4> case4, Action<T5> case5,
            Action<T6> case6, Action<T7> case7, Action caseNothing)
        {
            switch (_type)
            {
                case 1:
                    case1(_t1);
                    break;
                case 2:
                    case2(_t2);
                    break;
                case 3:
                    case3(_t3);
                    break;
                case 4:
                    case4(_t4);
                    break;
                case 5:
                    case5(_t5);
                    break;
                case 6:
                    case6(_t6);
                    break;
                case 7:
                    case7(_t7);
                    break;
                default:
                    caseNothing();
                    break;
            }
        }

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <returns>The result of the invoked delegate.</returns>
        public TResult Match<TResult>(Func<T1, TResult> case1, Func<T2, TResult> case2, Func<T3, TResult> case3,
            Func<T4, TResult> case4, Func<T5, TResult> case5, Func<T6, TResult> case6, Func<T7, TResult> case7,
            Func<TResult> caseNothing) =>
            _type switch
            {
                1 => case1(_t1),
                2 => case2(_t2),
                3 => case3(_t3),
                4 => case4(_t4),
                5 => case5(_t5),
                6 => case6(_t6),
                7 => case7(_t7),
                _ => caseNothing()
            };

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <returns>The result of the invoked delegate or Nothing.</returns>
        public Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>
            Match<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(Func<T1, TResult1> case1,
                Func<T2, TResult2> case2, Func<T3, TResult3> case3, Func<T4, TResult4> case4, Func<T5, TResult5> case5,
                Func<T6, TResult6> case6, Func<T7, TResult7> case7) =>
            _type switch
            {
                1 => Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>.Type1(case1(_t1)),
                2 => Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>.Type2(case2(_t2)),
                3 => Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>.Type3(case3(_t3)),
                4 => Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>.Type4(case4(_t4)),
                5 => Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>.Type5(case5(_t5)),
                6 => Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>.Type6(case6(_t6)),
                7 => Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>.Type7(case7(_t7)),
                _ => Union<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>.Nothing
            };

        /// <summary>
        /// Binds a function to T1 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T1 value.</param>
        /// <returns>An Option containing the result of the binding function if the Union contains a T1 value; otherwise, None.</returns>
        public Option<T> Bind1<T>(Func<T1, T> func) => _type == 1 ? Option<T>.Some(func(_t1)) : Option<T>.None;

        /// <summary>
        /// Binds a function to T2 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T2 value.</param>
        /// <returns>An Option containing the result of the binding function if the Union contains a T2 value; otherwise, None.</returns>
        public Option<T> Bind2<T>(Func<T2, T> func) => _type == 2 ? Option<T>.Some(func(_t2)) : Option<T>.None;

        /// <summary>
        /// Binds a function to T3 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T3 value.</param>
        /// <returns>An Option containing the result of the binding function if the Union contains a T3 value; otherwise, None.</returns>
        public Option<T> Bind3<T>(Func<T3, T> func) => _type == 3 ? Option<T>.Some(func(_t3)) : Option<T>.None;

        /// <summary>
        /// Binds a function to T4 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T4 value.</param>
        /// <returns>An Option containing the result of the binding function if the Union contains a T4 value; otherwise, None.</returns>
        public Option<T> Bind4<T>(Func<T4, T> func) => _type == 4 ? Option<T>.Some(func(_t4)) : Option<T>.None;

        /// <summary>
        /// Binds a function to T5 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T5 value.</param>
        /// <returns>An Option containing the result of the binding function if the Union contains a T5 value; otherwise, None.</returns>
        public Option<T> Bind5<T>(Func<T5, T> func) => _type == 5 ? Option<T>.Some(func(_t5)) : Option<T>.None;

        /// <summary>
        /// Binds a function to T6 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T6 value.</param>
        /// <returns>An Option containing the result of the binding function if the Union contains a T6 value; otherwise, None.</returns>
        public Option<T> Bind6<T>(Func<T6, T> func) => _type == 6 ? Option<T>.Some(func(_t6)) : Option<T>.None;
        
        /// <summary>
        /// Binds a function to T7 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T7 value.</param>
        /// <returns>An Option containing the result of the binding function if the Union contains a T7 value; otherwise, None.</returns>
        public Option<T> Bind7<T>(Func<T7, T> func) => _type == 7 ? Option<T>.Some(func(_t7)) : Option<T>.None;

        public override string ToString() => (_type switch
        {
            1 => _t1!.ToString(),
            2 => _t2!.ToString(),
            3 => _t3!.ToString(),
            4 => _t4!.ToString(),
            5 => _t5!.ToString(),
            6 => _t6!.ToString(),
            7 => _t7!.ToString(),
            _ => "Nothing"
        })!;
    }
}