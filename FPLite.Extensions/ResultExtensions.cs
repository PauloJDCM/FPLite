﻿using System;

namespace FPLite.Extensions
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Converts a value to a Result.
        /// </summary>
        public static Result<T, TError> ToResult<T, TError>(this T value, Func<TError> errorFunc)
            where TError : IError =>
            value is null ? Result<T, TError>.Err(errorFunc()) : Result<T, TError>.Ok(value);

        /// <summary>
        /// Converts a nullable value of type <typeparamref name="TIn"/> to an <see cref="Result{TOut, TError}"/>.
        /// </summary>
        public static Result<TOut, TError> AsResultOf<TIn, TOut, TError>(this TIn value, Func<TError> errorFunc)
            where TError : IError =>
            value is TOut cast ? Result<TOut, TError>.Ok(cast) : Result<TOut, TError>.Err(errorFunc());
    }
}