﻿using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace FPLite.Union
{
    /// <summary>
    /// Represents a discriminated union with two possible cases.
    /// </summary>
    public class Union<T1, T2, T3>
    {
        private readonly byte _type;
        private readonly T1 _t1;
        private readonly T2 _t2;
        private readonly T3 _t3;

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

        /// <summary>
        /// Represents a Union of 2 types with no value. Used to indicate the absence of a value in Union types.
        /// </summary>
        public static Union<T1, T2, T3> Nothing => new Union<T1, T2, T3>();

        /// <summary>
        /// Creates a Union with a value of Type 1, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t1">The value of Type 1 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3> Type1(T1 t1) => t1 is null ? Nothing : new Union<T1, T2, T3>(t1);

        /// <summary>
        /// Creates a Union with a value of Type 2, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t2">The value of Type 2 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3> Type2(T2 t2) => t2 is null ? Nothing : new Union<T1, T2, T3>(t2);

        /// <summary>
        /// Creates a Union with a value of Type 3, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t3">The value of Type 3 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3> Type3(T3 t3) => t3 is null ? Nothing : new Union<T1, T2, T3>(t3);

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action caseNothing)
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
            Func<TResult> caseNothing) =>
            _type switch
            {
                1 => case1(_t1),
                2 => case2(_t2),
                3 => case3(_t3),
                _ => caseNothing()
            };

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <returns>The result of the invoked delegate or Nothing.</returns>
        public Union<TResult1, TResult2, TResult3> Match<TResult1, TResult2, TResult3>(Func<T1, TResult1> case1,
            Func<T2, TResult2> case2, Func<T3, TResult3> case3) =>
            _type switch
            {
                1 => Union<TResult1, TResult2, TResult3>.Type1(case1(_t1)),
                2 => Union<TResult1, TResult2, TResult3>.Type2(case2(_t2)),
                3 => Union<TResult1, TResult2, TResult3>.Type3(case3(_t3)),
                _ => Union<TResult1, TResult2, TResult3>.Nothing
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

        public override string ToString() => (_type switch
        {
            1 => _t1!.ToString(),
            2 => _t2!.ToString(),
            3 => _t3!.ToString(),
            _ => "Nothing"
        })!;
    }
}