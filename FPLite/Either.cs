﻿using System;
using FPLite.Union;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace FPLite
{
    /// <summary>
    /// Represents a value that can be either Left(TLeft), Right(TRight) or Both(TLeft, TRight).
    /// </summary>
    /// <typeparam name="TLeft">The type of the left value.</typeparam>
    /// <typeparam name="TRight">The type of the right value.</typeparam>
    public class Either<TLeft, TRight> : IEquatable<Either<TLeft, TRight>>
    {
        public EitherType Type { get; }

        private readonly TLeft _left;
        private readonly TRight _right;

        protected Either()
        {
            Type = EitherType.Neither;
        }

        protected Either(TLeft left)
        {
            Type = EitherType.Left;
            _left = left;
        }

        protected Either(TRight right)
        {
            Type = EitherType.Right;
            _right = right;
        }

        protected Either(TLeft left, TRight right)
        {
            Type = EitherType.Both;
            _left = left;
            _right = right;
        }

        /// <summary>
        /// Gets an instance representing no value (Neither).
        /// </summary>
        public static Either<TLeft, TRight> Neither => new Either<TLeft, TRight>();

        /// <summary>
        /// Gets an instance representing a Left value if not null or Neither if null.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <returns>An Either instance representing a Left value or Neither.</returns>
        public static Either<TLeft, TRight> Left(TLeft left) =>
            left is null ? Neither : new Either<TLeft, TRight>(left);

        /// <summary>
        /// Gets an instance representing a Right value if not null or Neither if null.
        /// </summary>
        /// <param name="right">The right value.</param>
        /// <returns>An Either instance representing a Right value or Neither.</returns>
        public static Either<TLeft, TRight> Right(TRight right) =>
            right is null ? Neither : new Either<TLeft, TRight>(right);

        /// <summary>
        /// Creates an Either monad representing a choice between two values, allowing both values to be present.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="left">The nullable left value to include in the Either monad.</param>
        /// <param name="right">The nullable right value to include in the Either monad.</param>
        /// <returns>
        /// Either monad representing the choice between two values.<br/>
        /// If both left and right values are null, returns an instance representing "neither" value.<br/>
        /// If only the left value is present, returns an instance representing the left value.<br/>
        /// If only the right value is present, returns an instance representing the right value.<br/>
        /// If both left and right values are present, returns an instance representing both values.
        /// </returns>
        public static Either<TLeft, TRight> Both(TLeft left, TRight right) => (left, right) switch
        {
            (null, null) => new Either<TLeft, TRight>(),
            (_, null) => new Either<TLeft, TRight>(left),
            (null, _) => new Either<TLeft, TRight>(right),
            _ => new Either<TLeft, TRight>(left, right)
        };

        /// <summary>
        /// Matches the Either and executes an action based on whether it's Left, Right, Neither or Both.
        /// </summary>
        /// <param name="leftAction">The action to execute if it's Left.</param>
        /// <param name="rightAction">The action to execute if it's Right.</param>
        /// <param name="neitherAction">The action to execute if it's Neither.</param>
        /// <param name="bothAction">The action to execute if it's Both.</param>
        public void Match(Action<TLeft> leftAction, Action<TRight> rightAction, Action neitherAction,
            Action<TLeft, TRight> bothAction)
        {
            switch (Type)
            {
                case EitherType.Left:
                    leftAction(_left);
                    break;
                case EitherType.Right:
                    rightAction(_right);
                    break;
                case EitherType.Both:
                    bothAction(_left, _right);
                    break;
                case EitherType.Neither:
                default:
                    neitherAction();
                    break;
            }
        }

        /// <summary>
        /// Matches the Either and returns a result based on whether it's Left, Right, Neither or Both.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="leftFunc">The function to execute if it's Left.</param>
        /// <param name="rightFunc">The function to execute if it's Right.</param>
        /// <param name="neitherFunc">The function to execute if it's Neither.</param>
        /// <param name="bothFunc">The function to execute if it's Both.</param>
        /// <returns>The result of executing the appropriate function.</returns>
        public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
            Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc) => Type switch
        {
            EitherType.Left => leftFunc(_left),
            EitherType.Right => rightFunc(_right),
            EitherType.Both => bothFunc(_left, _right),
            _ => neitherFunc()
        };

        /// <summary>
        /// Matches the Either and returns a result based on whether it's Left, Right or Both.
        /// </summary>
        /// <typeparam name="TResultL">The left type of the result.</typeparam>
        /// <typeparam name="TResultR">The right type of the result.</typeparam>
        /// <param name="leftFunc">The function to execute if it's Left.</param>
        /// <param name="rightFunc">The function to execute if it's Right.</param>
        /// <param name="bothFunc">The function to execute if it's Both.</param>
        /// <returns>The result of executing the appropriate function or Neither.</returns>
        public Either<TResultL, TResultR> Match<TResultL, TResultR>(Func<TLeft, TResultL> leftFunc,
            Func<TRight, TResultR> rightFunc, Func<TLeft, TRight, Either<TResultL, TResultR>> bothFunc) => Type switch
        {
            EitherType.Left => Either<TResultL, TResultR>.Left(leftFunc(_left)),
            EitherType.Right => Either<TResultL, TResultR>.Right(rightFunc(_right)),
            EitherType.Both => bothFunc(_left, _right),
            _ => Either<TResultL, TResultR>.Neither
        };

        /// <summary>
        /// Binds a function to the left value of the Either type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the left value.</param>
        /// <returns>An Either containing the result of the binding function if the Either is of type Left or Both; otherwise, Right.</returns>
        public Either<T, TRight> BindLeft<T>(Func<TLeft, T> func) =>
            Type is EitherType.Left || Type is EitherType.Both
                ? Either<T, TRight>.Left(func(_left))
                : Either<T, TRight>.Right(_right);

        /// <summary>
        /// Binds a function to the right value of the Either type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to the right value.</param>
        /// <returns>An Either containing the result of the binding function if the Either is of type Right or Both; otherwise, Left.</returns>
        public Either<T, TLeft> BindRight<T>(Func<TRight, T> func) =>
            Type is EitherType.Right || Type is EitherType.Both
                ? Either<T, TLeft>.Left(func(_right))
                : Either<T, TLeft>.Right(_left);

        /// <summary>
        /// Binds a function to both the left and right values of the Either type.
        /// </summary>
        /// <typeparam name="T">The type of the result of the binding function.</typeparam>
        /// <param name="func">The function to bind to both values.</param>
        /// <returns>A Union containing the result of the binding function and the left and right values.</returns>
        public Union<T, TLeft, TRight> BindBoth<T>(Func<TLeft, TRight, T> func) => Type switch
        {
            EitherType.Both => Union<T, TLeft, TRight>.Type1(func(_left, _right)),
            EitherType.Left => Union<T, TLeft, TRight>.Type2(_left),
            EitherType.Right => Union<T, TLeft, TRight>.Type3(_right),
            _ => Union<T, TLeft, TRight>.Nothing
        };

        public override string ToString() => Type switch
        {
            EitherType.Left => $"Left({_left!.ToString()})",
            EitherType.Right => $"Right({_right!.ToString()})",
            EitherType.Both => $"Both({_left!.ToString()}, {_right!.ToString()})",
            _ => "Neither"
        };

        public override bool Equals(object? obj) => obj is Either<TLeft, TRight> other && Equals(other);

        public bool Equals(Either<TLeft, TRight>? other) => GetHashCode() == other?.GetHashCode();

        public override int GetHashCode() => HashCode.Combine(Type, _left, _right);

        public static bool operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right) => left.Equals(right);

        public static bool operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right) => !left.Equals(right);
    }
    
    public enum EitherType : byte
    {
        Neither,
        Left,
        Right,
        Both
    }
}