namespace FPLite.Types;

/// <summary>
/// Represents a value that can be either Left(TLeft) or Right(TRight).
/// </summary>
/// <typeparam name="TLeft">The type of the left value.</typeparam>
/// <typeparam name="TRight">The type of the right value.</typeparam>
public class Either<TLeft, TRight>
{
    private readonly TLeft? _left;
    private readonly TRight? _right;
    
    private Either()
    {
    }
    
    private Either(TLeft left)
    {
        _left = left;
    }
    
    private Either(TRight right)
    {
        _right = right;
    }

    /// <summary>
    /// Gets an instance representing no value (Neither).
    /// </summary>
    public static Either<TLeft, TRight> Neither => new();

    /// <summary>
    /// Gets an instance representing a Left value if not null or Neither if null.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <returns>An Either instance representing a Left value or Neither.</returns>
    public static Either<TLeft, TRight> Left(TLeft? left) => left is not null ? new(left) : Neither;

    /// <summary>
    /// Gets an instance representing a Right value if not null or Neither if null.
    /// </summary>
    /// <param name="right">The right value.</param>
    /// <returns>An Either instance representing a Right value or Neither.</returns>
    public static Either<TLeft, TRight> Right(TRight? right) => right is not null ? new(right) : Neither;

    /// <summary>
    /// Matches the either and returns a result based on whether it's Left or Right.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="leftFunc">The function to execute if it's Left.</param>
    /// <param name="rightFunc">The function to execute if it's Right.</param>
    /// <param name="neitherFunc">The function to execute if it's Neither.</param>
    /// <returns>The result of executing the appropriate function.</returns>
    public TResult Match<TResult>(Func<TLeft, TResult> leftFunc, Func<TRight, TResult> rightFunc,
        Func<TResult> neitherFunc)
    {
        if (_left is not null) return leftFunc(_left);
        if (_right is not null) return rightFunc(_right);
        return neitherFunc();
    }

    /// <summary>
    /// Matches the either and executes an action based on whether it's Left or Right.
    /// </summary>
    /// <param name="leftAction">The action to execute if it's Left.</param>
    /// <param name="rightAction">The action to execute if it's Right.</param>
    /// <param name="neitherAction">The action to execute if it's Neither.</param>
    public void Match(Action<TLeft> leftAction, Action<TRight> rightAction, Action neitherAction)
    {
        if (_left is not null) leftAction(_left);
        else if (_right is not null) rightAction(_right);
        else neitherAction();
    }

    /// <summary>
    /// Matches the either and returns a result based on whether it's Left or Right.
    /// </summary>
    /// <typeparam name="TResultL">The left type of the result.</typeparam>
    /// <typeparam name="TResultR">The right type of the result.</typeparam>
    /// <param name="leftFunc">The function to execute if it's Left.</param>
    /// <param name="rightFunc">The function to execute if it's Right.</param>
    /// <returns>The result of executing the appropriate function.</returns>
    public Either<TResultL, TResultR> Match<TResultL, TResultR>(Func<TLeft, TResultL> leftFunc,
        Func<TRight, TResultR> rightFunc)
    {
        if (_left is not null) return Either<TResultL, TResultR>.Left(leftFunc(_left));
        if (_right is not null) return Either<TResultL, TResultR>.Right(rightFunc(_right));
        return Either<TResultL, TResultR>.Neither;
    }

    /// <summary>
    /// Binds the either to a new either by applying a function to its Left value.
    /// </summary>
    /// <typeparam name="TNewLeft">The type of the left value in the new either.</typeparam>
    /// <param name="func">The function to apply to the Left value.</param>
    /// <returns>The new either resulting from the binding.</returns>
    public Either<TNewLeft, TRight> BindLeft<TNewLeft>(Func<TLeft, Either<TNewLeft, TRight>> func) =>
        _left is not null ? func(_left) : Either<TNewLeft, TRight>.Right(_right);

    /// <summary>
    /// Binds the either to a new either by applying a function to its Right value.
    /// </summary>
    /// <typeparam name="TNewRight">The type of the right value in the new either.</typeparam>
    /// <param name="func">The function to apply to the Right value.</param>
    /// <returns>The new either resulting from the binding.</returns>
    public Either<TLeft, TNewRight> BindRight<TNewRight>(Func<TRight, Either<TLeft, TNewRight>> func) =>
        _right is not null ? func(_right) : Either<TLeft, TNewRight>.Left(_left);

    /// <summary>
    /// Gets the value contained in the either monad.
    /// </summary>
    /// <returns>
    /// The value contained in the either monad. Returns null if both Left and Right values are null.
    /// </returns>
    public object? GetValue()
    {
        if (_left is not null) return _left;
        if (_right is not null) return _right;
        return null;
    }
}