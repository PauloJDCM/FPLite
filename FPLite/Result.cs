using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace FPLite
{
    public class Result<T, TError> where TError : IError
    {
        protected readonly bool IsOk;
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
        public static Result<T, TError> Err(TError error) => new Result<T, TError>(error);

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
        /// Unwraps the Result and returns its value or a default value.
        /// </summary>
        public T UnwrapOr(T defaultValue) => IsOk ? _value : defaultValue;

        public override string ToString() => IsOk ? $"Ok({_value!.ToString()})" : $"Error({_error.ToErrorString()})";
    }

    public class ResultCreateException<T> : Exception
    {
        private const string ErrorMessage = "Called Result<{0}>.Create() with a null value!";

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