namespace FPLite;

/// <summary>
/// Represents a value that can be either Left(TLeft), Right(TRight) or Both(TLeft, TRight).
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

    private Either(TLeft left, TRight right)
    {
        _left = left;
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
    public static Either<TLeft, TRight> Both(TLeft? left, TRight? right) => (left, right) switch
    {
        (null, null) => new(),
        (_, null) => new(left),
        (null, _) => new(right),
        _ => new(left, right)
    };

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
        Func<TResult> neitherFunc, Func<TLeft, TRight, TResult> bothFunc)
    {
        if (_left is not null && _right is not null) return bothFunc(_left, _right);
        if (_left is not null) return leftFunc(_left);
        if (_right is not null) return rightFunc(_right);
        return neitherFunc();
    }

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
        if (_left is not null && _right is not null) bothAction(_left, _right);
        else if (_left is not null) leftAction(_left);
        else if (_right is not null) rightAction(_right);
        else neitherAction();
    }

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
        Func<TRight, TResultR> rightFunc, Func<TLeft, TRight, Either<TResultL, TResultR>> bothFunc)
    {
        if (_left is not null && _right is not null) return bothFunc(_left, _right);
        if (_left is not null) return Either<TResultL, TResultR>.Left(leftFunc(_left));
        if (_right is not null) return Either<TResultL, TResultR>.Right(rightFunc(_right));
        return Either<TResultL, TResultR>.Neither;
    }

    /// <summary>
    /// Binds the Either to a new Either by applying a function to its Left value.
    /// </summary>
    /// <typeparam name="TNewLeft">The type of the left value in the new Either.</typeparam>
    /// <param name="func">The function to apply to the Left value.</param>
    /// <returns>The new Either resulting from the binding.</returns>
    public Either<TNewLeft, TRight> BindLeft<TNewLeft>(Func<TLeft, Either<TNewLeft, TRight>> func) =>
        _left is not null ? func(_left) : Either<TNewLeft, TRight>.Right(_right);

    /// <summary>
    /// Binds the Either to a new Either by applying a function to its Right value.
    /// </summary>
    /// <typeparam name="TNewRight">The type of the right value in the new Either.</typeparam>
    /// <param name="func">The function to apply to the Right value.</param>
    /// <returns>The new Either resulting from the binding.</returns>
    public Either<TLeft, TNewRight> BindRight<TNewRight>(Func<TRight, Either<TLeft, TNewRight>> func) =>
        _right is not null ? func(_right) : Either<TLeft, TNewRight>.Left(_left);

    /// <summary>
    /// Binds the Either to a new Either by applying a function to both values.
    /// </summary>
    /// <typeparam name="TNewLeft">The type of the left value in the new Either.</typeparam>
    /// <typeparam name="TNewRight">The type of the right value in the new Either.</typeparam>
    /// <param name="func">The function to apply to both values.</param>
    /// <returns>The new Either resulting from the binding or Neither.</returns>
    public Either<TNewLeft, TNewRight> BindBoth<TNewLeft, TNewRight>(
        Func<TLeft, TRight, Either<TNewLeft, TNewRight>> func) =>
        _left is not null && _right is not null ? func(_left, _right) : Either<TNewLeft, TNewRight>.Neither;

    /// <summary>
    /// Binds the Either to a new Either by applying a function to both values.
    /// </summary>
    /// <typeparam name="TNewLeft">The type of the left value in the new Either.</typeparam>
    /// <typeparam name="TNewRight">The type of the right value in the new Either.</typeparam>
    /// <param name="leftFunc">The function to apply to the left value.</param>
    /// <param name="rightFunc">The function to apply to the right value.</param>
    /// <returns>The new Either resulting from the binding or Neither.</returns>
    public Either<TNewLeft, TNewRight> BindBoth<TNewLeft, TNewRight>(
        Func<TLeft, TNewLeft> leftFunc, Func<TRight, TNewRight> rightFunc) =>
        (_left, _right) switch
        {
            (null, null) => Either<TNewLeft, TNewRight>.Neither,
            (_, null) => Either<TNewLeft, TNewRight>.Left(leftFunc(_left)),
            (null, _) => Either<TNewLeft, TNewRight>.Right(rightFunc(_right)),
            _ => Either<TNewLeft, TNewRight>.Both(leftFunc(_left), rightFunc(_right))
        };

    /// <summary>
    /// Gets the values contained in the Either monad.
    /// </summary>
    public (TLeft? _left, TRight? _right) GetValues() => (_left, _right);
}