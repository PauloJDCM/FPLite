namespace FPLite.Extensions;

public static class CommonExtensions
{
    /// <summary>
    /// Pipes the input value into the specified function, returning the result.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="input">The input value to be processed.</param>
    /// <param name="func">The function to apply to the input value.</param>
    /// <returns>The result of applying the function to the input value.</returns>
    public static TResult Pipe<T, TResult>(this T input, Func<T, TResult> func) => func(input);
    
    /// <summary>
    /// Pipes the input value into the specified action.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <param name="input">The input value to be used.</param>
    /// <param name="action">The action to run with the input value.</param>
    public static void Pipe<T>(this T input, Action<T> action) => action(input);

    /// <summary>
    /// Returns an empty function that ignores its input and performs no action.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter (ignored).</typeparam>
    /// <returns>An empty Action that ignores its input.</returns>
    public static Action<T> Ignore<T>() => _ =>
    {
        /* Empty action, does nothing */
    };

    /// <summary>
    /// Returns an empty action that performs no action.
    /// </summary>
    /// <returns>An empty Action that does nothing.</returns>
    public static Action Ignore() => () =>
    {
        /* Empty action, does nothing */
    };
}