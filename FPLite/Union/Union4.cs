﻿using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace FPLite.Union
{
    /// <summary>
    /// Represents a discriminated union with two possible cases.
    /// </summary>
    public class Union<T1, T2, T3, T4> : IEquatable<Union<T1, T2, T3, T4>>
    {
        public byte Type { get; }
        
        private readonly T1 _t1;
        private readonly T2 _t2;
        private readonly T3 _t3;
        private readonly T4 _t4;

        protected Union()
        {
        }

        protected Union(T1 t1)
        {
            Type = 1;
            _t1 = t1;
        }

        protected Union(T2 t2)
        {
            Type = 2;
            _t2 = t2;
        }

        protected Union(T3 t3)
        {
            Type = 3;
            _t3 = t3;
        }

        protected Union(T4 t4)
        {
            Type = 4;
            _t4 = t4;
        }

        /// <summary>
        /// Represents a Union of 2 types with no value. Used to indicate the absence of a value in Union types.
        /// </summary>
        public static Union<T1, T2, T3, T4> Nothing => new Union<T1, T2, T3, T4>();

        /// <summary>
        /// Creates a Union with a value of Type 1, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t1">The value of Type 1 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4> Type1(T1 t1) => t1 is null ? Nothing : new Union<T1, T2, T3, T4>(t1);

        /// <summary>
        /// Creates a Union with a value of Type 2, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t2">The value of Type 2 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4> Type2(T2 t2) => t2 is null ? Nothing : new Union<T1, T2, T3, T4>(t2);

        /// <summary>
        /// Creates a Union with a value of Type 3, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t3">The value of Type 3 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4> Type3(T3 t3) => t3 is null ? Nothing : new Union<T1, T2, T3, T4>(t3);

        /// <summary>
        /// Creates a Union with a value of Type 4, or returns Nothing if the provided value is null.
        /// </summary>
        /// <param name="t4">The value of Type 4 to be included in the Union, or null.</param>
        /// <returns>
        /// A Union containing the provided value if it is not null, or Nothing if the value is null.
        /// </returns>
        public static Union<T1, T2, T3, T4> Type4(T4 t4) => t4 is null ? Nothing : new Union<T1, T2, T3, T4>(t4);

        /// <summary>
        /// Matches the active case and invokes the appropriate action.
        /// </summary>
        public void Match(Action<T1> case1, Action<T2> case2, Action<T3> case3, Action<T4> case4, Action caseNothing)
        {
            switch (Type)
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
            Func<T4, TResult> case4, Func<TResult> caseNothing) =>
            Type switch
            {
                1 => case1(_t1),
                2 => case2(_t2),
                3 => case3(_t3),
                4 => case4(_t4),
                _ => caseNothing()
            };

        /// <summary>
        /// Matches the active case and invokes the appropriate delegate.
        /// </summary>
        /// <returns>The result of the invoked delegate or Nothing.</returns>
        public Union<TResult1, TResult2, TResult3, TResult4> Match<TResult1, TResult2, TResult3, TResult4>(
            Func<T1, TResult1> case1, Func<T2, TResult2> case2, Func<T3, TResult3> case3, Func<T4, TResult4> case4) =>
            Type switch
            {
                1 => Union<TResult1, TResult2, TResult3, TResult4>.Type1(case1(_t1)),
                2 => Union<TResult1, TResult2, TResult3, TResult4>.Type2(case2(_t2)),
                3 => Union<TResult1, TResult2, TResult3, TResult4>.Type3(case3(_t3)),
                4 => Union<TResult1, TResult2, TResult3, TResult4>.Type4(case4(_t4)),
                _ => Union<TResult1, TResult2, TResult3, TResult4>.Nothing
            };

        /// <summary>
        /// Binds a function to T1 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T1 value.</param>
        public Union<T, T2, T3, T4> Bind1<T>(Func<T1, T> func) => Type switch
        {
            1 => Union<T, T2, T3, T4>.Type1(func(_t1)),
            2 => Union<T, T2, T3, T4>.Type2(_t2),
            3 => Union<T, T2, T3, T4>.Type3(_t3),
            4 => Union<T, T2, T3, T4>.Type4(_t4),
            _ => Union<T, T2, T3, T4>.Nothing
        };

        /// <summary>
        /// Binds a function to T2 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T2 value.</param>
        public Union<T1, T, T3, T4> Bind2<T>(Func<T2, T> func) => Type switch
        {
            1 => Union<T1, T, T3, T4>.Type1(_t1),
            2 => Union<T1, T, T3, T4>.Type2(func(_t2)),
            3 => Union<T1, T, T3, T4>.Type3(_t3),
            4 => Union<T1, T, T3, T4>.Type4(_t4),
            _ => Union<T1, T, T3, T4>.Nothing
        };

        /// <summary>
        /// Binds a function to T3 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T3 value.</param>
        public Union<T1, T2, T, T4> Bind3<T>(Func<T3, T> func) => Type switch
        {
            1 => Union<T1, T2, T, T4>.Type1(_t1),
            2 => Union<T1, T2, T, T4>.Type2(_t2),
            3 => Union<T1, T2, T, T4>.Type3(func(_t3)),
            4 => Union<T1, T2, T, T4>.Type4(_t4),
            _ => Union<T1, T2, T, T4>.Nothing
        };

        /// <summary>
        /// Binds a function to T4 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T4 value.</param>
        public Union<T1, T2, T3, T> Bind4<T>(Func<T4, T> func) => Type switch
        {
            1 => Union<T1, T2, T3, T>.Type1(_t1),
            2 => Union<T1, T2, T3, T>.Type2(_t2),
            3 => Union<T1, T2, T3, T>.Type3(_t3),
            4 => Union<T1, T2, T3, T>.Type4(func(_t4)),
            _ => Union<T1, T2, T3, T>.Nothing
        };

        public override string ToString() => (Type switch
        {
            1 => $"T1({_t1!.ToString()})",
            2 => $"T2({_t2!.ToString()})",
            3 => $"T3({_t3!.ToString()})",
            4 => $"T4({_t4!.ToString()})",
            _ => "Nothing"
        })!;
        
        public override bool Equals(object? obj) => obj is Union<T1, T2, T3, T4> other && Equals(other);

        public bool Equals(Union<T1, T2, T3, T4>? other) => GetHashCode() == other?.GetHashCode();

        public override int GetHashCode() => HashCode.Combine(Type, _t1, _t2, _t3, _t4);

        public static bool operator ==(Union<T1, T2, T3, T4> left, Union<T1, T2, T3, T4> right) => left.Equals(right);

        public static bool operator !=(Union<T1, T2, T3, T4> left, Union<T1, T2, T3, T4> right) => !left.Equals(right);
    }
}