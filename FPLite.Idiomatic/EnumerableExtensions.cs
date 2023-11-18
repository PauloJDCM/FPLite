namespace FPLite.Idiomatic
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Filters elements in the source sequence based on a specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The source sequence to filter.</param>
        /// <param name="predicate">A function that determines whether each element should be included.</param>
        /// <returns>An IEnumerable&lt;T&gt; containing the filtered elements.</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
            source.Where(predicate);

        /// <summary>
        /// Maps elements in the source sequence to a new type using a specified mapper function.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of elements in the resulting sequence.</typeparam>
        /// <param name="source">The source sequence to map.</param>
        /// <param name="mapper">A function that transforms each element to a new type.</param>
        /// <returns>An IEnumerable&lt;TResult&gt; containing the mapped elements.</returns>
        public static IEnumerable<TResult> Map<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> mapper) => source.Select(mapper);

        /// <summary>
        /// Collects and flattens elements in the source sequence using a specified collector function.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of elements in the resulting sequence.</typeparam>
        /// <param name="source">The source sequence to collect and flatten.</param>
        /// <param name="collector">A function that returns a sequence for each source element.</param>
        /// <returns>An IEnumerable&lt;TResult&gt; containing the collected and flattened elements.</returns>
        public static IEnumerable<TResult> Collect<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> collector) => source.SelectMany(collector);

        /// <summary>
        /// Reduces the elements in the source sequence using a specified reducer function.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence to reduce.</param>
        /// <param name="reducer">A function that combines two elements into one.</param>
        /// <returns>The result of reducing the source sequence.</returns>
        public static T Reduce<T>(this IEnumerable<T> source, Func<T, T, T> reducer) => source.Aggregate(reducer);

        /// <summary>
        /// Folds the elements in the source sequence with an initial seed and a specified folder function.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TAcc">The type of the accumulator (seed and result).</typeparam>
        /// <param name="source">The source sequence to fold.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="folder">A function that combines the accumulator and each element.</param>
        /// <returns>The result of folding the source sequence.</returns>
        public static TAcc Fold<T, TAcc>(this IEnumerable<T> source, TAcc seed, Func<TAcc, T, TAcc> folder) =>
            source.Aggregate(seed, folder);

        /// <summary>
        /// Initializes a sequence of elements based on a specified count and initializer function.
        /// </summary>
        /// <typeparam name="T">The type of elements to generate.</typeparam>
        /// <param name="count">The number of elements to generate.</param>
        /// <param name="initializer">A function that creates an element based on its index.</param>
        /// <returns>An IEnumerable&lt;T&gt; containing the generated elements.</returns>
        public static IEnumerable<T> Init<T>(int count, Func<int, T> initializer) =>
            Enumerable.Range(0, count).Select(initializer);
    }
}