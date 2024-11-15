﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using FPLite.Option;
using static FPLite.Extensions.OptionExtensions;

namespace FPLite.Extensions;

public static class OptionEnumerableExtensions
{
    /// <summary>
    /// Returns the first element of a sequence, satisfying a specified predicate, 
    /// if such exists.
    /// </summary>
    /// <returns>An <see cref="Option{T}"/> instance containing the first element if present.</returns>
    [Pure]
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> source, Predicate<T> predicate)
        where T : notnull =>
        TryOption(() => source.First(x => predicate(x)));

    /// <summary>
    /// Returns the first element of a sequence, if such exists.
    /// </summary>
    /// <returns>An <see cref="Option{T}"/> instance containing the first element if present.</returns>
    [Pure]
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> source)
        where T : notnull =>
        TryOption(source.First);

    /// <summary>
    /// Returns the last element of a sequence, satisfying a specified predicate, 
    /// if such exists.
    /// </summary>
    /// <returns>An <see cref="Option{T}"/> instance containing the last element if present.</returns>
    [Pure]
    public static Option<T> LastOrNone<T>(this IEnumerable<T> source, Predicate<T> predicate)
        where T : notnull =>
        TryOption(() => source.Last(x => predicate(x)));

    /// <summary>
    /// Returns the last element of a sequence, if such exists.
    /// </summary>
    /// <returns>An <see cref="Option{T}"/> instance containing the last element if present.</returns>
    [Pure]
    public static Option<T> LastOrNone<T>(this IEnumerable<T> source)
        where T : notnull =>
        TryOption(source.Last);

    /// <summary>
    /// Returns a single element from a sequence, satisfying a specified predicate, 
    /// if it exists and is the only element in the sequence.
    /// </summary>
    /// <returns>An <see cref="Option{T}"/> instance containing the element if present.</returns>
    [Pure]
    public static Option<T> SingleOrNone<T>(this IEnumerable<T> source, Predicate<T> predicate)
        where T : notnull =>
        TryOption(() => source.Single(x => predicate(x)));

    /// <summary>
    /// Returns a single element from a sequence, if it exists and is the only element in the sequence.
    /// </summary>
    /// <returns>An <see cref="Option{T}"/> instance containing the element if present.</returns>
    [Pure]
    public static Option<T> SingleOrNone<T>(this IEnumerable<T> source)
        where T : notnull =>
        TryOption(source.Single);

    /// <summary>
    /// Returns an element at a specified position in a sequence if such exists.
    /// </summary>
    /// <returns>An <see cref="Option{T}"/> instance containing the element if found.</returns>
    [Pure]
    public static Option<T> ElementAtOrNone<T>(this IEnumerable<T> source, int index)
        where T : notnull =>
        TryOption(() => source.ElementAt(index));

    /// <summary>
    /// Returns the value associated with the specified key if such exists.
    /// A dictionary lookup will be used if available, otherwise falling
    /// back to a linear scan of the enumerable.
    /// </summary>
    /// <returns>An <see cref="Option{TValue}"/> instance containing the associated value if located.</returns>
    [Pure]
    public static Option<TValue> GetValueOrNone<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source,
        TKey key)
        where TValue : notnull =>
        source switch
        {
            IDictionary<TKey, TValue> dictionary => dictionary.TryGetValue(key, out var value)
                ? Option<TValue>.Some(value)
                : Option<TValue>.None(),
            _ => source.FirstOrNone(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                .Match(
                    pair => Option<TValue>.Some(pair.Value),
                    () => Option<TValue>.None()
                )
        };
}