#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace FPLite.Types
{
    /// <summary>
    /// Represents a value that can be either Left(TLeft) or Right(TRight).
    /// </summary>
    /// <typeparam name="TLeft">The type of the left value.</typeparam>
    /// <typeparam name="TRight">The type of the right value.</typeparam>
    public class Either<TLeft, TRight>
    {
        private readonly TLeft _left;
        private readonly TRight _right;

        /// <summary>
        /// Initializes a new instance of the Either class as Left.
        /// </summary>
        /// <param name="left">The left value.</param>
        private Either(TLeft left)
        {
            _left = left;
            IsRight = false;
        }

        /// <summary>
        /// Initializes a new instance of the Either class as Right.
        /// </summary>
        /// <param name="right">The right value.</param>
        private Either(TRight right)
        {
            _right = right;
            IsRight = true;
        }

        /// <summary>
        /// Gets an instance representing a Left value.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <returns>An Either instance representing a Left value.</returns>
        public static Either<TLeft, TRight> Left(TLeft left) => new(left);

        /// <summary>
        /// Gets an instance representing a Right value.
        /// </summary>
        /// <param name="right">The right value.</param>
        /// <returns>An Either instance representing a Right value.</returns>
        public static Either<TLeft, TRight> Right(TRight right) => new(right);

        /// <summary>
        /// Gets a value indicating whether this instance represents a Left value.
        /// </summary>
        public bool IsLeft => !IsRight;

        /// <summary>
        /// Gets a value indicating whether this instance represents a Right value.
        /// </summary>
        public bool IsRight { get; }

        /// <summary>
        /// Matches the either and returns a result based on whether it's Left or Right.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="leftFunc">The function to execute if it's Left.</param>
        /// <param name="rightFunc">The function to execute if it's Right.</param>
        /// <returns>The result of executing the appropriate function.</returns>
        public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc) =>
            IsRight ? rightFunc(_right) : leftFunc(_left);

        /// <summary>
        /// Matches the either and executes an action based on whether it's Left or Right.
        /// </summary>
        /// <param name="leftAction">The action to execute if it's Left.</param>
        /// <param name="rightAction">The action to execute if it's Right.</param>
        public void Match(Action<TLeft> leftAction, Action<TRight> rightAction)
        {
            if (IsRight)
                rightAction(_right);
            else
                leftAction(_left);
        }

        /// <summary>
        /// Binds the either to a new either by applying a function to its Left value.
        /// </summary>
        /// <typeparam name="TNewLeft">The type of the left value in the new either.</typeparam>
        /// <param name="func">The function to apply to the Left value.</param>
        /// <returns>The new either resulting from the binding.</returns>
        public Either<TNewLeft, TRight> BindLeft<TNewLeft>(Func<TLeft, Either<TNewLeft, TRight>> func) =>
            IsRight ? Either<TNewLeft, TRight>.Right(_right) : func(_left);

        /// <summary>
        /// Binds the either to a new either by applying a function to its Right value.
        /// </summary>
        /// <typeparam name="TNewRight">The type of the right value in the new either.</typeparam>
        /// <param name="func">The function to apply to the Right value.</param>
        /// <returns>The new either resulting from the binding.</returns>
        public Either<TLeft, TNewRight> BindRight<TNewRight>(Func<TRight, Either<TLeft, TNewRight>> func) =>
            IsRight ? func(_right) : Either<TLeft, TNewRight>.Left(_left);
    }
}