namespace FPLite.Extensions;

public static class OptionExtensions
{
    /// <summary>
    /// Converts a nullable value to an Option type.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The nullable value to be converted.</param>
    /// <returns>An <see cref="Option{T}"/> instance containing the associated value if value is not null
    /// or an instance representing no value.</returns>
    public static Option<T> ToOption<T>(this T? value) => value is null ? Option<T>.None : Option<T>.Some(value);

    /// <summary>
    /// Converts a nullable value of type <typeparamref name="TIn"/> to an <see cref="Option{TOut}"/>.
    /// </summary>
    /// <typeparam name="TIn">The input type, a nullable type.</typeparam>
    /// <typeparam name="TOut">The output type, a reference type.</typeparam>
    /// <param name="value">The nullable value to be converted.</param>
    /// <returns>
    /// An <see cref="Option{TOut}"/> containing the non-null casted value if it is of type <typeparamref name="TOut"/>,
    /// or an empty <see cref="Option{TOut}"/> if the value is null or not of the expected type.
    /// </returns>
    public static Option<TOut> AsOptionOf<TIn, TOut>(this TIn? value) where TOut : class =>
        value is TOut cast ? Option<TOut>.Some(cast) : Option<TOut>.None;
}