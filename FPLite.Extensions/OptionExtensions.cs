using System;

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

        /// <summary>
        /// Tries to execute a function and returns a <see cref="Option{T}"/> with the result.
        /// </summary>
        public static Option<T> TryOption<T>(Func<T> func)
        {
            try
            {
                return Option<T>.Some(func());
            }
            catch
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Tries to execute an action and returns a <see cref="Option{TError}"/> with the result if an exception is thrown.
        /// </summary>
        public static Option<TError> TryOption<TError>(Action action, Func<Exception, TError> failFunc)
            where TError : IError
        {
            try
            {
                action();
                return Option<TError>.None;
            }
            catch (Exception e)
            {
                return Option<TError>.Some(failFunc(e));
            }
        }
    }
}