namespace Result.Net
{
    using System.Collections.Generic;

    /// <summary>
    /// the result class with a data property
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
        public Result(TData data, ResultStatus status)
            : base(status)
        {
            Data = data;
        }

        /// <summary>
        /// check if the operation associated with this result has produce a data.
        /// </summary>
        public override bool HasData() => !EqualityComparer<TData>.Default.Equals(Data, default);

        /// <summary>
        /// implicit operator for converting from a result to it data
        /// </summary>
        /// <param name="result">the result instance</param>
        public static implicit operator TData(Result<TData> result)
            => result is null || !result.HasData() ? (default) : result.Data;

        /// <summary>
        /// implicit operator for converting from a data to a result
        /// </summary>
        /// <param name="data">the data instance</param>
        public static implicit operator Result<TData>(TData data) => Success(data);
    }
}

