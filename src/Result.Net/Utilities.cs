namespace Result.Net
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// a utilities class to hold common logic
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// check if the given string is not null or empty or white space
        /// </summary>
        /// <param name="value">the string value to be checked</param>
        /// <returns>true if valid, false if not</returns>
        internal static bool IsValid(this string value)
            => !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));

        /// <summary>
        /// generate a random error code for log tracing
        /// </summary>
        /// <returns>the generated error code</returns>
        internal static string GenerateLogTraceErrorCode()
            => Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("=", "").Replace("+", "").Replace("/", "").Replace("\\", "");

        /// <summary>
        /// build a list of <typeparamref name="TSource"/> from a given type
        /// </summary>
        /// <typeparam name="TSource">the type of the object</typeparam>
        /// <param name="source">the source object</param>
        /// <param name="nextItem">the next item</param>
        /// <param name="canContinue">a check if we can continue</param>
        /// <returns>the list of objects</returns>
        internal static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem, Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        /// <summary>
        /// build a list of <typeparamref name="TSource"/> from a given type
        /// </summary>
        /// <typeparam name="TSource">the type of the object</typeparam>
        /// <param name="source">the source object</param>
        /// <param name="nextItem">the next item</param>
        /// <returns>the list of objects</returns>
        internal static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem)
            where TSource : class => FromHierarchy(source, nextItem, s => s != null);
    }
}
