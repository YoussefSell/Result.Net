namespace Result.Net
{
    using System.Collections.Generic;

    /// <summary>
    /// the result class with a value property
    /// </summary>
    public partial class Result<TValue> : Result
    {
        /// <summary>
        /// the result of the operation
        /// </summary>
        public TValue Value { get; }
    }

    /// <summary>
    /// partial part for <see cref="Result{TValue}"/>
    /// </summary>
    public partial class Result<TValue> : Result
    {
        /// <summary>
        /// create an instance of the <see cref="Result{TValue}"/> with the given value, and status
        /// </summary>
        /// <param name="value">the value associated with the result</param>
        /// <param name="status">the status of the result</param>
        public Result(TValue value, ResultStatus status)
            : base(status)
        {
            Value = value;
        }

        /// <summary>
        /// check if the operation associated with this result has produce a value
        /// </summary>
        public override bool HasValue() => !EqualityComparer<TValue>.Default.Equals(Value, default);

        /// <summary>
        /// implicit operator for converting from a result to it value
        /// </summary>
        /// <param name="result">the result instance</param>
        public static implicit operator TValue(Result<TValue> result)
            => result is null || !result.HasValue() ? (default) : result.Value;

        /// <summary>
        /// implicit operator for converting from a value to a result
        /// </summary>
        /// <param name="value">the value instance</param>
        public static implicit operator Result<TValue>(TValue value) => Success(value);
    }
}

