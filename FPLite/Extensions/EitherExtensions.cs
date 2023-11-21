using FPLite.Types;

namespace FPLite.Extensions;

public static class EitherExtensions
{
    /// <summary>
    /// Converts a nullable left value to an Either monad.
    /// </summary>
    /// <typeparam name="TLeft">The type of the left value.</typeparam>
    /// <typeparam name="TRight">The type of the right value.</typeparam>
    /// <param name="left">The nullable left value to convert.</param>
    /// <returns>
    /// Either monad representing the conversion result.
    /// If the provided left value is null, returns a representation of "neither" value.
    /// Otherwise, returns a representation of the left value.
    /// </returns>
    public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this TLeft? left) =>
        left is null ? Either<TLeft, TRight>.Neither : Either<TLeft, TRight>.Left(left);
}