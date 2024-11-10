using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using FPLite.Result;
using static FPLite.Extensions.ResultExtensions;

namespace FPLite.Extensions
{
    public static class ResultEnumerableExtensions
    {
        /// <summary>
        /// Returns the first element of a sequence, satisfying a specified predicate, 
        /// if such exists.
        /// </summary>
        /// <returns>An <see cref="Result{T,Exception}"/> instance containing the first element if present.</returns>
        [Pure]
        public static Result<T, Exception> FirstOrError<T>(this IEnumerable<T> source, Predicate<T> predicate)
            where T : notnull =>
            TryResult(() => source.First(x => predicate(x)));

        /// <summary>
        /// Returns the first element of a sequence, if such exists.
        /// </summary>
        /// <returns>An <see cref="Result{T,Exception}"/> instance containing the first element if present.</returns>
        [Pure]
        public static Result<T, Exception> FirstOrError<T>(this IEnumerable<T> source)
            where T : notnull =>
            TryResult(source.First);

        /// <summary>
        /// Returns the last element of a sequence, satisfying a specified predicate, 
        /// if such exists.
        /// </summary>
        /// <returns>An <see cref="Result{T,Exception}"/> instance containing the last element if present.</returns>
        [Pure]
        public static Result<T, Exception> LastOrError<T>(this IEnumerable<T> source, Predicate<T> predicate)
            where T : notnull =>
            TryResult(() => source.Last(x => predicate(x)));

        /// <summary>
        /// Returns the last element of a sequence, if such exists.
        /// </summary>
        /// <returns>An <see cref="Result{T,Exception}"/> instance containing the last element if present.</returns>
        [Pure]
        public static Result<T, Exception> LastOrError<T>(this IEnumerable<T> source)
            where T : notnull =>
            TryResult(source.Last);

        /// <summary>
        /// Returns a single element from a sequence, satisfying a specified predicate, 
        /// if it exists and is the only element in the sequence.
        /// </summary>
        /// <returns>An <see cref="Result{T,Exception}"/> instance containing the element if present.</returns>
        [Pure]
        public static Result<T, Exception> SingleOrError<T>(this IEnumerable<T> source, Predicate<T> predicate)
            where T : notnull =>
            TryResult(() => source.Single(x => predicate(x)));

        /// <summary>
        /// Returns a single element from a sequence, if it exists and is the only element in the sequence.
        /// </summary>
        /// <returns>An <see cref="Result{T,Exception}"/> instance containing the element if present.</returns>
        [Pure]
        public static Result<T, Exception> SingleOrError<T>(this IEnumerable<T> source)
            where T : notnull =>
            TryResult(source.Single);

        /// <summary>
        /// Returns an element at a specified position in a sequence if such exists.
        /// </summary>
        /// <returns>An <see cref="Result{T,Exception}"/> instance containing the element if found.</returns>
        [Pure]
        public static Result<T, Exception> ElementAtOrError<T>(this IEnumerable<T> source, int index)
            where T : notnull =>
            TryResult(() => source.ElementAt(index));

        /// <summary>
        /// Returns the value associated with the specified key if such exists.
        /// A dictionary lookup will be used if available, otherwise falling
        /// back to a linear scan of the enumerable.
        /// </summary>
        /// <returns>An <see cref="Result{TValue, KeyNotFoundException}"/> instance containing the associated value if located.</returns>
        [Pure]
        public static Result<TValue, KeyNotFoundException> GetValueOrError<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key)
            where TValue : notnull =>
            source switch
            {
                IDictionary<TKey, TValue> dictionary => dictionary.TryGetValue(key, out var value)
                    ? Result<TValue, KeyNotFoundException>.Ok(value)
                    : Result<TValue, KeyNotFoundException>.Err(new KeyNotFoundException(key?.ToString())),
                _ => source.FirstOrNone(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                    .Match(
                        pair => Result<TValue, KeyNotFoundException>.Ok(pair.Value),
                        () => Result<TValue, KeyNotFoundException>.Err(new KeyNotFoundException(key?.ToString()))
                    )
            };
    }
}