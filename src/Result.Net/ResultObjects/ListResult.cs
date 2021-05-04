namespace Result.Net
{ 
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// the List result implementation class
    /// </summary>
    /// <typeparam name="TValue">the type of the data to be returned</typeparam>
    public partial class ListResult<TValue>
    {
        /// <summary>
        /// the count on the items in the list
        /// </summary>
        public int Count => Value?.Count ?? 0;
    }

    /// <summary>
    /// partial part for <see cref="ListResult{TValue}"/>
    /// </summary>
    public partial class ListResult<TValue> : Result<IReadOnlyCollection<TValue>>
    {
        /// <summary>
        /// create an instance of the <see cref="ListResult{TValue}"/> object with given status, message, code, logTraceCode, and errors
        /// </summary>
        /// <param name="value">the value associated with the result</param>
        /// <param name="status">the status of the result</param>
        /// <param name="message">the message associated with the result</param>
        /// <param name="code">the code associated with the result</param>
        /// <param name="logTraceCode">the logTraceCode associated with the result</param>
        /// <param name="errors">the errors associated with the result</param>
        public ListResult(IReadOnlyCollection<TValue> value, ResultStatus status) 
            : base(value, status) { }

        /// <inheritdoc/>
        public static implicit operator ListResult<TValue>(List<TValue> result) => ListSuccess(result);

        /// <inheritdoc/>
        public static implicit operator ListResult<TValue>(TValue[] result) => ListSuccess(result);

        /// <inheritdoc/>
        public static implicit operator ListResult<TValue>(LinkedList<TValue> result) => ListSuccess(result);

        /// <inheritdoc/>
        public static implicit operator ListResult<TValue>(HashSet<TValue> result) => ListSuccess(result);
    }
}

