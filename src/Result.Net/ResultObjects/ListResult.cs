namespace Result.Net
{ 
    using System.Collections.Generic;

    /// <summary>
    /// a result object that defines a list result output.
    /// </summary>
    /// <typeparam name="TData">the type of the data to be returned</typeparam>
    public partial class ListResult<TData>
    {
        /// <summary>
        /// the count on the items in the list
        /// </summary>
        public int Count => Data?.Count ?? 0;
    }

    /// <summary>
    /// partial part for <see cref="ListResult{TData}"/>
    /// </summary>
    public partial class ListResult<TData> : Result<IReadOnlyCollection<TData>>
    {
        /// <summary>
        /// create an instance of the <see cref="ListResult{TData}"/> object with given status, message, code, logTraceCode, and errors
        /// </summary>
        /// <param name="data">the data associated with the result</param>
        /// <param name="status">the status of the result</param>
        public ListResult(IReadOnlyCollection<TData> data, ResultStatus status) : base(data, status) { }

        /// <inheritdoc/>
        public static implicit operator ListResult<TData>(List<TData> result) => ListSuccess(result);

        /// <inheritdoc/>
        public static implicit operator ListResult<TData>(TData[] result) => ListSuccess(result);

        /// <inheritdoc/>
        public static implicit operator ListResult<TData>(LinkedList<TData> result) => ListSuccess(result);

        /// <inheritdoc/>
        public static implicit operator ListResult<TData>(HashSet<TData> result) => ListSuccess(result);
    }
}

