using System;
using FPLite.Union;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace FPLite
{
    public class Result<T, TError> where TError : IError
    {
        public bool IsOk { get; }

        private readonly T _value;
        private readonly TError _error;

        protected Result(T value)
        {
            IsOk = true;
            _value = value;
        }

        protected Result(TError error)
        {
            _error = error;
        }

        /// <summary>
        /// Creates an instance representing a value (Ok(T)).
        /// </summary>
        /// <exception cref="ResultCreateException{T}"> Thrown if the value is null. </exception>
        public static Result<T, TError> Ok(T value) =>
            value is null ? throw new ResultCreateException<T>() : new Result<T, TError>(value);

        /// <summary>
        /// Creates an instance representing an error (Error(TError)).
        /// </summary>
        /// <exception cref="ResultCreateException{TError}"> Thrown if the error is null. </exception>
        public static Result<T, TError> Err(TError error) =>
            error is null ? throw new ResultCreateException<TError>() : new Result<T, TError>(error);

        /// <summary>
        /// Matches the Result and executes a function based on whether it's Ok or Error.
        /// </summary>
        /// <typeparam name="TResult"> The type of the function result. </typeparam>
        /// <param name="okFunc"> The function to execute if it's Ok. </param>
        /// <param name="errorFunc"> The function to execute if it's Error. </param>
        /// <returns> The result of the function. </returns>
        public TResult Match<TResult>(Func<T, TResult> okFunc, Func<TError, TResult> errorFunc) =>
            IsOk ? okFunc(_value) : errorFunc(_error);

        /// <summary>
        /// Matches the Result and executes an action based on whether it's Ok or Error.
        /// </summary>
        /// <param name="okAction"> The action to execute if it's Ok. </param>
        /// <param name="errorAction"> The action to execute if it's Error. </param>
        public void Match(Action<T> okAction, Action<TError> errorAction)
        {
            switch (IsOk)
            {
                case true:
                    okAction(_value);
                    break;
                case false:
                    errorAction(_error);
                    break;
            }
        }

        /// <summary>
        /// Binds the Result to a new Result by applying a function to its value.
        /// </summary>
        /// <param name="func"> The function to apply to the value.</param>
        /// <typeparam name="TResult"> The type of the result.</typeparam>
        /// <returns> The new Result resulting from the binding.</returns>
        public Result<TResult, TError> Bind<TResult>(Func<T, TResult> func) =>
            IsOk ? Result<TResult, TError>.Ok(func(_value)) : Result<TResult, TError>.Err(_error);

        /// <summary>
        /// Unwraps the Result and returns its value.
        /// </summary>
        /// <exception cref="ResultUnwrapException{T,TError}"> Thrown if the Result is an Error. </exception>
        public T Unwrap() => IsOk ? _value : throw new ResultUnwrapException<T, TError>(_error);

        /// <summary>
        /// Unwraps the Result and returns its value or throws an exception if it's an Error.
        /// </summary>
        /// <typeparam name="TException"> The type of the exception. </typeparam>
        /// <param name="exceptionFunc"> The function to execute if it's an Error. </param>
        /// <returns> The value if it's Ok. </returns>
        public T Unwrap<TException>(Func<TException> exceptionFunc) where TException : Exception =>
            IsOk ? _value : throw exceptionFunc();

        /// <summary>
        /// Unwraps the Result and returns its value or executes a function if it's an Error.
        /// </summary>
        /// <param name="otherFunc"> The function to execute if it's an Error. </param>
        /// <returns> The value if it's Ok or the function result if it's an Error. </returns>
        public T UnwrapOr(Func<T> otherFunc) => IsOk ? _value : otherFunc();

        /// <summary>
        /// Unwraps the Result and returns its value or executes a function if it's an Error.
        /// </summary>
        /// <typeparam name="TOther"> The type of the function result. </typeparam>
        /// <param name="otherFunc"> The function to execute if it's an Error. </param>
        /// <returns> A Union containing the value if it's Ok or the function result if it's an Error. </returns>
        public Union<T, TOther> UnwrapOr<TOther>(Func<TOther> otherFunc) =>
            IsOk ? Union<T, TOther>.Type1(_value) : Union<T, TOther>.Type2(otherFunc());

        public override string ToString() => IsOk ? $"Ok({_value!.ToString()})" : $"Err({_error.ToErrorString()})";
    }

    public class ResultCreateException<T> : Exception
    {
        private const string ErrorMessage = "Called Result.Create({0}) with a null value!";

        public ResultCreateException() : base(string.Format(ErrorMessage, typeof(T)))
        {
        }
    }

    public class ResultUnwrapException<T, TError> : Exception where TError : IError
    {
        public TError Error { get; }

        private const string ErrorMessage = "Called Result<{0}>.Unwrap() on an Error!";

        public ResultUnwrapException(TError error) : base(string.Format(ErrorMessage, typeof(T)))
        {
            Error = error;
        }
    }
}