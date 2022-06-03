namespace ResultNet
{
    using System.Collections.Generic;

    /// <summary>
    /// this class defines a result object without a data. 
    /// use it to describe the status of an operation with no return data
    /// </summary>
    public partial class Result
    {
        /// <summary>
        /// Get the Status of the result.
        /// </summary>
        public ResultStatus Status { get; }

        /// <summary>
        /// Get or set the message that describes the result of the operation.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Get or set the error code that describes the result of the operation.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Get or set a unique log trace code used to trace the result in logs.
        /// </summary>
        public string LogTraceCode { get; set; }

        /// <summary>
        /// Get the list of errors associated with the operation result.
        /// </summary>
        public ICollection<ResultError> Errors { get; }

        /// <summary>
        /// Get a collection of key/data pairs that provide additional 
        /// meta data information about the operation result.
        /// </summary>
        public IDictionary<string, object> MetaData { get; }
    }

    /// <summary>
    /// partial part for <see cref="Result"/>
    /// </summary>
    public partial class Result
    {
        /// <summary>
        /// create an instance of the Result object with given status, message, code and errors
        /// </summary>
        /// <param name="status">the status of the result</param>
        /// <param name="message">the message associated with the result</param>
        /// <param name="code">the code associated with the result</param>
        /// <param name="errors">the list of errors if any</param>
        public Result(ResultStatus status, string message, string code, params ResultError[] errors)
            : this(status, message, code, status == ResultStatus.Failed ? Utilities.GenerateLogTraceErrorCode() : "", errors) { }

        /// <summary>
        /// create an instance of the Result object with given status, message, code and errors
        /// </summary>
        /// <param name="status">the status of the result</param>
        /// <param name="message">the message associated with the result</param>
        /// <param name="code">the code associated with the result</param>
        /// <param name="logTraceCode">the log trace code associated with the result</param>
        /// <param name="errors">the list of errors if any</param>
        public Result(ResultStatus status, string message, string code, string logTraceCode, params ResultError[] errors)
            : this(status)
        {
            Code = code;
            Message = message;
            LogTraceCode = logTraceCode;
            Errors = new LinkedList<ResultError>(errors);
        }

        /// <summary>
        /// create an instance of the Result object with given status, the message, code, and logTraceId 
        /// will be set to default data base on the result status type.
        /// </summary>
        /// <param name="status">the status of the result</param>
        public Result(ResultStatus status)
        {
            Status = status;
            Message = string.Empty;
            LogTraceCode = string.Empty;
            Code = ResultCode.OperationSucceeded;
            Errors = new LinkedList<ResultError>();
            MetaData = new Dictionary<string, object>();

            if (Status == ResultStatus.Failed)
            {
                Code = ResultCode.OperationFailed;
                LogTraceCode = Utilities.GenerateLogTraceErrorCode();
            }
        }

        /// <summary>
        /// check if the operation associated with this result has produce a data.
        /// </summary>
        public virtual bool HasData() => false;

        /// <inheritdoc/>
        public override string ToString() => $"{Status} | code: {Code}";
    }
}

