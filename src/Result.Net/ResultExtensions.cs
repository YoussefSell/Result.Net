namespace Result.Net
{
    using System;
    using System.Linq;

    /// <summary>
    /// a set of extensions methods on top of the result object
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// set the message related to this result instance
        /// </summary>
        /// <param name="message">the message value</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithMessage<TResult>(this TResult result, string message) where TResult : Result
        {
            result.Message = message;
            return result;
        }

        /// <summary>
        /// set the code related to this result instance
        /// </summary>
        /// <param name="code">the code value</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithCode<TResult>(this TResult result, string code) where TResult : Result
        {
            result.Code = code;
            return result;
        }

        /// <summary>
        /// set the log trace Code related to this result instance
        /// </summary>
        /// <param name="traceCode">the trace Code value</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithLogTraceCode<TResult>(this TResult result, string traceCode) where TResult : Result
        {
            result.LogTraceCode = traceCode;
            return result;
        }

        /// <summary>
        /// set the log trace Code related to this result instance
        /// </summary>
        /// <param name="errors">the trace Code value</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithErrors<TResult>(this TResult result, params ResultError[] errors) where TResult : Result
        {
            foreach (var error in errors)
            {
                result.Errors.Add(error);
            }

            return result;
        }

        /// <summary>
        /// set the exception related to this result instance
        /// </summary>
        /// <param name="exception">the exception instance</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithErrors<TResult>(this TResult result, Exception exception) where TResult : Result
        {
            foreach (var error in ResultError.GetFromException(exception))
            {
                result.Errors.Add(error);
            }

            return result;
        }

        /// <summary>
        /// is this Result has any errors associated with it.
        /// </summary>
        public static bool HasErrors(this Result result) => !(result.Errors is null) && result.Errors.Any();

        /// <summary>
        /// true if there is no ResultErrors and the status of the result is Success.
        /// </summary>
        public static bool IsSuccess(this Result result) => result.Status == ResultStatus.Succeed && !result.HasErrors();

        /// <summary>
        /// check if the given list empty
        /// </summary>
        public static bool IsListEmpty<TValue>(this ListResult<TValue> result) => result.Count <= 0;
    }
}
