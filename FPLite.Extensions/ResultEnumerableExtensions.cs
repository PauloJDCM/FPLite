using System;
using System.Collections.Generic;
using System.Linq;

namespace FPLite.Extensions
{
    public static class ResultEnumerableExtensions
    {
        /// <summary>
        /// Returns the first element of a sequence, satisfying a specified predicate, 
        /// if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the first element from.</param>
        /// <param name="predicate">The predicate to filter by.</param>
        /// <param name="errorFunc">The function to return an error if the predicate fails.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the first element if present.</returns>
        public static Result<T, TError> FirstOrNone<T, TError>(this IEnumerable<T> source, Predicate<T> predicate,
            Func<TError> errorFunc)
            where TError : IError
        {
            try
            {
                var result = source.First(arg => predicate(arg));
                return Result<T, TError>.Ok(result);
            }
            catch
            {
                return Result<T, TError>.Err(errorFunc());
            }
        }

        /// <summary>
        /// Returns the first element of a sequence, if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the first element from.</param>
        /// <param name="errorFunc">The function to return an error if the predicate fails.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the first element if present.</returns>
        public static Result<T, TError> FirstOrNone<T, TError>(this IEnumerable<T> source, Func<TError> errorFunc)
            where TError : IError
        {
            try
            {
                var result = source.First();
                return Result<T, TError>.Ok(result);
            }
            catch
            {
                return Result<T, TError>.Err(errorFunc());
            }
        }

        /// <summary>
        /// Returns the last element of a sequence, satisfying a specified predicate, 
        /// if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the last element from.</param>
        /// <param name="predicate">The predicate to filter by.</param>
        /// <param name="errorFunc">The function to return an error if the predicate fails.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the last element if present.</returns>
        public static Result<T, TError> LastOrNone<T, TError>(this IEnumerable<T> source, Predicate<T> predicate,
            Func<TError> errorFunc)
            where TError : IError
        {
            try
            {
                var result = source.Last(arg => predicate(arg));
                return Result<T, TError>.Ok(result);
            }
            catch
            {
                return Result<T, TError>.Err(errorFunc());
            }
        }

        /// <summary>
        /// Returns the last element of a sequence, if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the last element from.</param>
        /// <param name="errorFunc">The function to return an error if the predicate fails.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the last element if present.</returns>
        public static Result<T, TError> LastOrNone<T, TError>(this IEnumerable<T> source, Func<TError> errorFunc)
            where TError : IError
        {
            try
            {
                var result = source.Last();
                return Result<T, TError>.Ok(result);
            }
            catch
            {
                return Result<T, TError>.Err(errorFunc());
            }
        }

        /// <summary>
        /// Returns a single element from a sequence, satisfying a specified predicate, 
        /// if it exists and is the only element in the sequence.
        /// </summary>
        /// <param name="source">The sequence to return the element from.</param>
        /// <param name="predicate">The predicate to filter by.</param>
        /// <param name="errorFunc">The function to return an error if the predicate fails.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the element if present.</returns>
        public static Result<T, TError> SingleOrNone<T, TError>(this IEnumerable<T> source, Predicate<T> predicate,
            Func<TError> errorFunc)
            where TError : IError
        {
            try
            {
                var result = source.Single(arg => predicate(arg));
                return Result<T, TError>.Ok(result);
            }
            catch
            {
                return Result<T, TError>.Err(errorFunc());
            }
        }

        /// <summary>
        /// Returns a single element from a sequence, if it exists and is the only element in the sequence.
        /// </summary>
        /// <param name="source">The sequence to return the element from.</param>
        /// <param name="errorFunc">The function to return an error if the predicate fails.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the element if present.</returns>
        public static Result<T, TError> SingleOrNone<T, TError>(this IEnumerable<T> source, Func<TError> errorFunc)
            where TError : IError
        {
            try
            {
                var result = source.Single();
                return Result<T, TError>.Ok(result);
            }
            catch
            {
                return Result<T, TError>.Err(errorFunc());
            }
        }

        /// <summary>
        /// Returns an element at a specified position in a sequence if such exists.
        /// </summary>
        /// <param name="source">The sequence to return the element from.</param>
        /// <param name="index">The index in the sequence.</param>
        /// <param name="errorFunc">The function to return an error if the predicate fails.</param>
        /// <returns>An <see cref="Option{T}"/> instance containing the element if found.</returns>
        public static Result<T, TError> ElementAtOrNone<T, TError>(this IEnumerable<T> source, int index,
            Func<TError> errorFunc)
            where TError : IError
        {
            try
            {
                var result = source.ElementAt(index);
                return Result<T, TError>.Ok(result);
            }
            catch
            {
                return Result<T, TError>.Err(errorFunc());
            }
        }

        /// <summary>
        /// Returns the value associated with the specified key if such exists.
        /// A dictionary lookup will be used if available, otherwise falling
        /// back to a linear scan of the enumerable.
        /// </summary>
        /// <param name="source">The dictionary or enumerable in which to locate the key.</param>
        /// <param name="key">The key to locate.</param>
        /// <param name="errorFunc">The function to return an error if the predicate fails.</param>
        /// <returns>An <see cref="Option{TValue}"/> instance containing the associated value if located.</returns>
        public static Result<TValue, TError> GetValueOrNone<TKey, TValue, TError>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source,
            TKey key, Func<TError> errorFunc)
            where TError : IError =>
            source switch
            {
                IDictionary<TKey, TValue> dictionary => dictionary.TryGetValue(key, out var value)
                    ? Result<TValue, TError>.Ok(value)
                    : Result<TValue, TError>.Err(errorFunc()),
                _ => source.FirstOrNone(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                    .Match(
                        pair => Result<TValue, TError>.Ok(pair.Value),
                        () => Result<TValue, TError>.Err(errorFunc())
                    )
            };
    }
}