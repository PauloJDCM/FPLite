using FPLite.Types;

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
}