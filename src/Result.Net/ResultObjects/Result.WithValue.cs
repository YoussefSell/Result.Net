namespace ResultNet
{
    using System.Collections.Generic;

    /// <summary>
    /// the result class with a data property.
    /// </summary>
    /// <typeparam name="TData">the type of the data.</typeparam>
    public partial class Result<TData> : Result
    {
        /// <summary>
        /// the data associated with result.
        /// </summary>
        public TData Data { get; }
    }

    /// <summary>
    /// partial part for <see cref="Result{TData}"/>
    /// </summary>
    public partial class Result<TData> : Result
    {
        /// <summary>
        /// create an instance of the <see cref="Result{TData}"/> with the given data, and status
        /// </summary>
        /// <param name="data">the data associated with the result</param>
        /// <param name="status">the status of the result</param>
        /// <param name="message">the message associated with the result</param>
        /// <param name="code">the code associated with the result</param>
        /// <param name="logTraceCode">the log trace code associated with the result</param>
        /// <param name="errors">the list of errors if any</param>
        /// <param name="metaData">the collection of meta-data if any</param>
        public Result(TData data, ResultStatus status, string message, string code, string logTraceCode, ICollection<ResultError> errors, IDictionary<string, object> metaData)
            : base(status, message, code, logTraceCode, errors, metaData) => Data = data;

        /// <summary>
        /// check if the operation associated with this result has produce a data.
        /// </summary>
        public override bool HasData() => !EqualityComparer<TData>.Default.Equals(Data, default);

        /// <inheritdoc/>
        public static implicit operator TData(Result<TData> result)
            => result is null || !result.HasData() ? (default) : result.Data;

        /// <inheritdoc/>
        public static implicit operator Result<TData>(TData data) => Success(data);
    }
}

