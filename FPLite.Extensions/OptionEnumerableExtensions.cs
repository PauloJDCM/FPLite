namespace FPLite.Extensions;

public static class OptionEnumerableExtensions
{
    /// <summary>
    /// Returns the first element of a sequence, satisfying a specified predicate, 
    /// if such exists.
    /// </summary>
    /// <param name="source">The sequence to return the first element from.</param>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An <see cref="Option{T}"/> instance containing the first element if present.</returns>
    public static Option<T> FirstOrNone<T>(this IEnumerable<T>? source, Predicate<T>? predicate = null)
    {
        if (source is null) return Option<T>.None;

        return predicate is null
            ? source.FirstOrDefault().ToOption()
            : source.FirstOrDefault(arg => predicate(arg)).ToOption();
    }

    /// <summary>
    /// Returns the last element of a sequence, satisfying a specified predicate, 
    /// if such exists.
    /// </summary>
    /// <param name="source">The sequence to return the last element from.</param>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An <see cref="Option{T}"/> instance containing the last element if present.</returns>
    public static Option<T> LastOrNone<T>(this IEnumerable<T>? source, Predicate<T>? predicate = null)
    {
        if (source is null) return Option<T>.None;

        return predicate is null
            ? source.LastOrDefault().ToOption()
            : source.LastOrDefault(arg => predicate(arg)).ToOption();
    }

    /// <summary>
    /// Returns a single element from a sequence, satisfying a specified predicate, 
    /// if it exists and is the only element in the sequence.
    /// </summary>
    /// <param name="source">The sequence to return the element from.</param>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An <see cref="Option{T}"/> instance containing the element if present.</returns>
    public static Option<T> SingleOrNone<T>(this IEnumerable<T>? source, Predicate<T>? predicate = null)
    {
        if (source is null) return Option<T>.None;

        return predicate is null
            ? source.SingleOrDefault().ToOption()
            : source.SingleOrDefault(arg => predicate(arg)).ToOption();
    }

    /// <summary>
    /// Returns an element at a specified position in a sequence if such exists.
    /// </summary>
    /// <param name="source">The sequence to return the element from.</param>
    /// <param name="index">The index in the sequence.</param>
    /// <returns>An <see cref="Option{T}"/> instance containing the element if found.</returns>
    public static Option<T> ElementAtOrNone<T>(this IEnumerable<T>? source, int index) =>
        source is null
            ? Option<T>.None
            : source.ElementAtOrDefault(index).ToOption();

    /// <summary>
    /// Returns the value associated with the specified key if such exists.
    /// A dictionary lookup will be used if available, otherwise falling
    /// back to a linear scan of the enumerable.
    /// </summary>
    /// <param name="source">The dictionary or enumerable in which to locate the key.</param>
    /// <param name="key">The key to locate.</param>
    /// <returns>An <see cref="Option{TValue}"/> instance containing the associated value if located.</returns>
    public static Option<TValue> GetValueOrNone<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source,
        TKey key) =>
        source switch
        {
            null => Option<TValue>.None,
            IDictionary<TKey, TValue> dictionary => dictionary.TryGetValue(key, out var value)
                ? Option<TValue>.Some(value)
                : Option<TValue>.None,
            IReadOnlyDictionary<TKey, TValue> readOnlyDictionary => readOnlyDictionary.TryGetValue(key, out var value)
                ? Option<TValue>.Some(value)
                : Option<TValue>.None,
            _ => source.FirstOrNone(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                .Match(pair => pair.Value.ToOption(), () => Option<TValue>.None)
        };
}