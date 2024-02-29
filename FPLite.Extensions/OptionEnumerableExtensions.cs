using System;
using System.Collections.Generic;
using System.Linq;

namespace FPLite.Extensions
{
    public static class OptionEnumerableExtensions
    {
        /// <summary>
        /// Returns the first element of a sequence, satisfying a specified predicate, 
        /// if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the first element from.</param>
        /// <param name="predicate">The predicate to filter by.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the first element if present.</returns>
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            try
            {
                var result = source.First(arg => predicate(arg));
                return Option<T>.Some(result);
            }
            catch
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Returns the first element of a sequence, if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the first element from.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the first element if present.</returns>
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> source)
        {
            try
            {
                var result = source.First();
                return Option<T>.Some(result);
            }
            catch
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Returns the last element of a sequence, satisfying a specified predicate, 
        /// if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the last element from.</param>
        /// <param name="predicate">The predicate to filter by.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the last element if present.</returns>
        public static Option<T> LastOrNone<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            try
            {
                var result = source.Last(arg => predicate(arg));
                return Option<T>.Some(result);
            }
            catch
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Returns the last element of a sequence, if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the last element from.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the last element if present.</returns>
        public static Option<T> LastOrNone<T>(this IEnumerable<T> source)
        {
            try
            {
                var result = source.Last();
                return Option<T>.Some(result);
            }
            catch
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Returns a single element from a sequence, satisfying a specified predicate, 
        /// if it exists and is the only element in the sequence.
        /// </summary>
        /// <param name="source">The sequence to return the element from.</param>
        /// <param name="predicate">The predicate to filter by.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the element if present.</returns>
        public static Option<T> SingleOrNone<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            try
            {
                var result = source.Single(arg => predicate(arg));
                return Option<T>.Some(result);
            }
            catch
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Returns a single element from a sequence, if it exists and is the only element in the sequence.
        /// </summary>
        /// <param name="source">The sequence to return the element from.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the element if present.</returns>
        public static Option<T> SingleOrNone<T>(this IEnumerable<T> source)
        {
            try
            {
                var result = source.Single();
                return Option<T>.Some(result);
            }
            catch
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Returns an element at a specified position in a sequence if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the element from.</param>
        /// <param name="index">The index in the sequence.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the element if found.</returns>
        public static Option<T> ElementAtOrNone<T>(this IEnumerable<T> source, int index)
        {
            try
            {
                var result = source.ElementAt(index);
                return Option<T>.Some(result);
            }
            catch
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Returns the value associated with the specified key if such exists.
        /// A dictionary lookup will be used if available, otherwise falling
        /// back to a linear scan of the enumerable.
        /// </summary>
        /// <param name="source">The dictionary or enumerable in which to locate the key.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>An <see cref="Option{TValue}"/> instance containing the associated value if located.</returns>
        public static Option<TValue> GetValueOrNone<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source,
            TKey key) =>
            source switch
            {
                null => Option<TValue>.None,
                IDictionary<TKey, TValue> dictionary => dictionary.TryGetValue(key, out var value)
                    ? Option<TValue>.Some(value)
                    : Option<TValue>.None,
                IReadOnlyDictionary<TKey, TValue> readOnlyDictionary => readOnlyDictionary.TryGetValue(key,
                    out var value)
                    ? Option<TValue>.Some(value)
                    : Option<TValue>.None,
                _ => source.FirstOrNone(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                    .Match(
                        pair => Option<TValue>.Some(pair.Value),
                        () => Option<TValue>.None
                    )
            };
    }
}