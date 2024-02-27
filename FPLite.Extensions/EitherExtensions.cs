namespace FPLite.Extensions
{
    public static class EitherExtensions
    {
        /// <summary>
        /// Converts value to an Either monad with a left value.
        /// </summary>
        public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this TLeft left) =>
            Either<TLeft, TRight>.Left(left);

        /// <summary>
        /// Converts value to an Either monad with a right value.
        /// </summary>
        public static Either<TLeft, TRight> ToEither<TLeft, TRight>(this TRight right) =>
            Either<TLeft, TRight>.Right(right);
    }
}