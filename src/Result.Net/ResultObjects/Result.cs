namespace Result.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// this class defines a result object without a value. 
    /// use it to describe the status of an operation with no return value
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
        public ICollection<ResultError> Errors { get; set; }
    }

    /// <summary>
    /// partial part for <see cref="Result"/>
    /// </summary>
    public partial class Result
    {
        /// <summary>
        /// create an instance of the Result object with given status, the message, code, and logTraceId 
        /// will be set to default value base on the result status type.
        /// </summary>
        /// <param name="status">the status of the result</param>
        public Result(ResultStatus status)
        {
            Status = status;
            LogTraceCode = string.Empty;
            Errors = new LinkedList<ResultError>();

            if (Status == ResultStatus.Succeed)
            {
                Message = "Operation Succeeded";
                Code = ResultCode.OperationSucceeded;
                return;
            }

            Message = "Operation Failed";
            Code = ResultCode.OperationFailed;
            LogTraceCode = Utilities.GenerateLogTraceErrorCode();
        }

        /// <summary>
        /// check if the operation associated with this result has produce a value.
        /// </summary>
        public virtual bool HasValue() => false;

        /// <inheritdoc/>
        public override string ToString() => $"{Message} | Status: {Status}, code: {Code}";
    }
}

