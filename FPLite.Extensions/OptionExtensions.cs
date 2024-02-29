namespace FPLite.Extensions
{
    public static class OptionExtensions
    {
        /// <summary>
        /// Converts a nullable value to an Option type.
        /// </summary>
        public static Option<T> ToOption<T>(this T value) => Option<T>.Some(value);

        /// <summary>
        /// Converts a value of type <typeparamref name="TIn"/> to an <see cref="Option{TOut}"/>.
        /// </summary>
        public static Option<TOut> AsOptionOf<TIn, TOut>(this TIn value) =>
            value is TOut cast ? Option<TOut>.Some(cast) : Option<TOut>.None;
    }
}