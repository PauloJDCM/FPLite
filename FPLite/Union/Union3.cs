﻿using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace FPLite.Union
{
    /// <summary>
    /// Represents a discriminated union with two possible cases.
    /// </summary>
    public class Union<T1, T2, T3> : IEquatable<Union<T1, T2, T3>>
    {
        public byte Type { get; }
        
        private readonly T1 _t1;
        private readonly T2 _t2;
        private readonly T3 _t3;

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
            Type switch
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
            Type switch
            {
                1 => Union<TResult1, TResult2, TResult3>.Type1(case1(_t1)),
                2 => Union<TResult1, TResult2, TResult3>.Type2(case2(_t2)),
                3 => Union<TResult1, TResult2, TResult3>.Type3(case3(_t3)),
                _ => Union<TResult1, TResult2, TResult3>.Nothing
            };

        // Generate xml documentation
        /// <summary>
        /// Binds a function to T1 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T1 value.</param>
        public Union<T, T2, T3> Bind1<T>(Func<T1, T> func) => Type switch
        {
            1 => Union<T, T2, T3>.Type1(func(_t1)),
            2 => Union<T, T2, T3>.Type2(_t2),
            3 => Union<T, T2, T3>.Type3(_t3),
            _ => Union<T, T2, T3>.Nothing
        };

        /// <summary>
        /// Binds a function to T2 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T2 value.</param>
        public Union<T1, T, T3> Bind2<T>(Func<T2, T> func) => Type switch
        {
            1 => Union<T1, T, T3>.Type1(_t1),
            2 => Union<T1, T, T3>.Type2(func(_t2)),
            3 => Union<T1, T, T3>.Type3(_t3),
            _ => Union<T1, T, T3>.Nothing
        };

        /// <summary>
        /// Binds a function to T3 of the Union type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the T3 value.</param>
        public Union<T1, T2, T> Bind3<T>(Func<T3, T> func) => Type switch
        {
            1 => Union<T1, T2, T>.Type1(_t1),
            2 => Union<T1, T2, T>.Type2(_t2),
            3 => Union<T1, T2, T>.Type3(func(_t3)),
            _ => Union<T1, T2, T>.Nothing
        };

        public override string ToString() => (Type switch
        {
            1 => $"T1({_t1!.ToString()})",
            2 => $"T2({_t2!.ToString()})",
            3 => $"T3({_t3!.ToString()})",
            _ => "Nothing"
        })!;
        
        public override bool Equals(object? obj) => obj is Union<T1, T2, T3> other && Equals(other);

        public bool Equals(Union<T1, T2, T3>? other) => GetHashCode() == other?.GetHashCode();

        public override int GetHashCode() => HashCode.Combine(Type, _t1, _t2, _t3);

        public static bool operator ==(Union<T1, T2, T3> left, Union<T1, T2, T3> right) => left.Equals(right);

        public static bool operator !=(Union<T1, T2, T3> left, Union<T1, T2, T3> right) => !left.Equals(right);
    }
}