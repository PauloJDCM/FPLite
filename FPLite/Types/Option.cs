namespace FPLite.Types
{
    /// <summary>
    /// Represents an optional value that can be either Some(T) or None.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class Option<T>
    {
        private readonly T? _value;

        /// <summary>
        /// Initializes a new instance of the Option class with no value (None).
        /// </summary>
        private Option()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Option class with a value (Some(T)).
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        private Option(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets an instance representing no value (None).
        /// </summary>
        public static Option<T> None => new();

        /// <summary>
        /// Creates an instance representing a value (Some(T)).
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        /// <returns>An Option containing the specified value.</returns>
        public static Option<T> Some(T? value) => value is not null ? new(value) : None;

        /// <summary>
        /// Matches the option and returns a result based on whether it's Some or None.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="someFunc">The function to execute if it's Some.</param>
        /// <param name="noneFunc">The function to execute if it's None.</param>
        /// <returns>The result of executing the appropriate function.</returns>
        public TResult Match<TResult>(Func<T, TResult> someFunc, Func<TResult> noneFunc) =>
            _value is not null ? someFunc(_value) : noneFunc();

        /// <summary>
        /// Matches the option and executes an action based on whether it's Some or None.
        /// </summary>
        /// <param name="someAction">The action to execute if it's Some.</param>
        /// <param name="noneAction">The action to execute if it's None.</param>
        public void Match(Action<T> someAction, Action noneAction)
        {
            if (_value is not null)
                someAction(_value);
            else
                noneAction();
        }

        /// <summary>
        /// Binds the option to a new option by applying a function to its value.
        /// </summary>
        /// <typeparam name="TResult">The type of the result option.</typeparam>
        /// <param name="func">The function to apply to the value.</param>
        /// <returns>The new option resulting from the binding.</returns>
        public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> func) =>
            _value is not null ? func(_value) : Option<TResult>.None;

        /// <summary>
        /// Retrieves the nullable value wrapped in the Option.
        /// </summary>
        /// <returns>
        /// The nullable value wrapped in the Option.
        /// </returns>
        public T? ToNullable() => _value;
    }
}