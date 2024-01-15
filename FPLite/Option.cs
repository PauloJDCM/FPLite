using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace FPLite
{
    /// <summary>
    /// Represents an optional value that can be either Some(T) or None.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class Option<T>
    {
        private readonly bool _isSome;
        private readonly T _value;

        private Option()
        {
        }

        private Option(T value)
        {
            _isSome = true;
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
            if (_isSome)
                someAction(_value!);
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
            _isSome ? someFunc(_value!) : noneFunc();

        /// <summary>
        /// Binds the Option to a new Option by applying a function to its value.
        /// </summary>
        /// <typeparam name="TResult">The type of the result Option.</typeparam>
        /// <param name="func">The function to apply to the value.</param>
        /// <returns>The new Option resulting from the binding.</returns>
        public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> func) =>
            _isSome ? func(_value!) : Option<TResult>.None;
        
        public override string ToString() => (_isSome ? _value!.ToString() : "None")!;
    }
}