namespace Result.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// this class defines a result object without a data. 
    /// use it to describe the status of an operation with no return data
    /// </summary>
    public partial class Result
    {
        /// <summary>
        /// the Status of the result
        /// </summary>
        public ResultStatus Status { get; }

        /// <summary>
        /// get the message associated with this result, in case of an ResultError
        /// this property will hold the ResultError description
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// a code that represent a message, used to identify the ResultError
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// a unique log trace code used to trace the result in logs
        /// </summary>
        public string LogTraceCode { get; set; }

        /// <summary>
        /// this list of errors associated with the failure of the operation
        /// </summary>
        public ICollection<ResultError> Errors { get; }

        /// <summary>
        /// Gets a collection of key/data pairs that provide additional 
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
                return;
            }
        }

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
            Errors = errors;
            Message = message;
            LogTraceCode = logTraceCode;
        }

        /// <summary>
        /// check if the operation associated with this result has produce a data.
        /// </summary>
        public virtual bool HasData() => false;

        /// <inheritdoc/>
        public override string ToString() => $"{Status} | code: {Code}";
    }
}

