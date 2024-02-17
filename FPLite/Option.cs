using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace FPLite
{
    /// <summary>
    /// Represents an optional value that can be either Some(T) or None.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class Option<T> : IEquatable<Option<T>>
    {
        protected readonly bool IsSome;
        private readonly T _value;

        protected Option()
        {
        }

        protected Option(T value)
        {
            IsSome = true;
            _value = value;
        }

        /// <summary>
        /// Gets an instance representing no value (None).
        /// </summary>
        public static Option<T> None => new Option<T>();

        /// <summary>
        /// Creates an instance representing a value (Some(T)).
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        /// <returns>An Option containing the specified value.</returns>
        public static Option<T> Some(T value) => value is null ? None : new Option<T>(value);

        /// <summary>
        /// Matches the Option and executes an action based on whether it's Some or None.
        /// </summary>
        /// <param name="someAction">The action to execute if it's Some.</param>
        /// <param name="noneAction">The action to execute if it's None.</param>
        public void Match(Action<T> someAction, Action noneAction)
        {
            if (IsSome)
                someAction(_value);
            else
                noneAction();
        }

        /// <summary>
        /// Matches the Option and returns a result based on whether it's Some or None.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="someFunc">The function to execute if it's Some.</param>
        /// <param name="noneFunc">The function to execute if it's None.</param>
        /// <returns>The result of executing the appropriate function.</returns>
        public TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc) =>
            IsSome ? someFunc(_value!) : noneFunc();

        /// <summary>
        /// Binds the Option to a new Option by applying a function to its value.
        /// </summary>
        /// <typeparam name="TResult">The type of the result Option.</typeparam>
        /// <param name="func">The function to apply to the value.</param>
        /// <returns>The new Option resulting from the binding.</returns>
        public Option<TResult> Bind<TResult>(Func<T, TResult> func) =>
            IsSome ? Option<TResult>.Some(func(_value!)) : Option<TResult>.None;

        /// <summary>
        /// Unwraps the Option and returns its value.
        /// </summary>
        /// <exception cref="InvalidOperationException"> Thrown if the Option is None. </exception>
        public T Unwrap() => IsSome ? _value! : throw new OptionUnwrapException<T>();
        
        /// <summary>
        /// Unwraps the Option and returns its value or a default value.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the Option is None.</param>
        public T UnwrapOr(T defaultValue) => IsSome ? _value! : defaultValue;

        public override string ToString() => (IsSome ? _value!.ToString() : "None")!;

        public override bool Equals(object? obj) => obj is Option<T> option && Equals(option);

        public bool Equals(Option<T>? other) => GetHashCode() == other?.GetHashCode();

        public override int GetHashCode() => HashCode.Combine(IsSome, _value);

        public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);

        public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);
    }
    
    public class OptionUnwrapException<T> : Exception
    {
        private const string ErrorMessage = "Called Option<{0}>.Unwrap() on None!.";
        
        public OptionUnwrapException() : base(string.Format(ErrorMessage, typeof(T)))
        {
        }
    }
}