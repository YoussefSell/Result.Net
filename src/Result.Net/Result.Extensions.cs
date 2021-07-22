namespace Result.Net
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// a set of extensions methods on top of the result object
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// set the message related to this result instance
        /// </summary>
        /// <param name="message">the message data</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithMessage<TResult>(this TResult result, string message) where TResult : Result
        {
            result.Message = message;
            return result;
        }

        /// <summary>
        /// set the code related to this result instance
        /// </summary>
        /// <param name="code">the code data</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithCode<TResult>(this TResult result, string code) where TResult : Result
        {
            result.Code = code;
            return result;
        }

        /// <summary>
        /// set the log trace Code related to this result instance
        /// </summary>
        /// <param name="traceCode">the trace Code data</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithLogTraceCode<TResult>(this TResult result, string traceCode) where TResult : Result
        {
            result.LogTraceCode = traceCode;
            return result;
        }

        /// <summary>
        /// set the log trace Code related to this result instance
        /// </summary>
        /// <param name="errors">the trace Code data</param>
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
        /// adds an element with the provided key and data to the data list of the result instance
        /// </summary>
        /// <typeparam name="TResult">the type of result instance</typeparam>
        /// <param name="result">the result object instance</param>
        /// <param name="key">the key of the data</param>
        /// <param name="data">the data instance</param>
        /// <returns>the instance of result to enable method chaining</returns>
        public static TResult WithData<TResult>(this TResult result, string key, object data) where TResult : Result
        {
            result.MetaData.Add(key, data);
            return result;
        }

        /// <summary>
        /// Determines whether the Result object instance has any errors associated with it.
        /// </summary>
        public static bool HasErrors(this Result result) 
            => !(result is null) && !(result.Errors is null) && result.Errors.Any();

        /// <summary>
        /// Determines whether the Result object instance has any metaData associated with it.
        /// </summary>
        public static bool HasMetaData(this Result result)
            => !(result is null) && !(result.MetaData is null) && result.MetaData.Any();

        /// <summary>
        /// Determines whether the Result object instance represent a successful result.
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <returns>true if successful result, otherwise false</returns>
        public static bool IsSuccess(this Result result) 
            => !(result is null) && result.Status == ResultStatus.Succeed;

        /// <summary>
        ///Determines whether the Result object instance represent a successful result.
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <returns>true if failure result, otherwise false</returns>
        public static bool IsFailure(this Result result) 
            => !(result is null) && result.Status == ResultStatus.Failed;

        /// <summary>
        /// check if the given list empty
        /// </summary>
        public static bool IsListEmpty<TData>(this ListResult<TData> result) => result.Count <= 0;

        /// <summary>
        /// convert the result instance to an exception.
        /// </summary>
        /// <typeparam name="TResult">the result type</typeparam>
        /// <param name="result">the result object instance</param>
        /// <returns>an instance of the exception.</returns>
        public static ResultException ToException<TResult>(this TResult result)
            where TResult : Result => ToException<TResult, ResultException>(result);

        /// <summary>
        /// convert the result instance to an exception.
        /// </summary>
        /// <typeparam name="TResult">the result type</typeparam>
        /// <typeparam name="TException">the exception type</typeparam>
        /// <param name="result">the result object instance</param>
        /// <returns>an instance of the exception.</returns>
        public static TException ToException<TResult, TException>(this TResult result) 
            where TException : ResultException
            where TResult : Result
        {
            var mapper = ResultExceptionMapper<TException>.GetMapping(result.Code);
            if (mapper is null)
                return new ResultException(result) as TException;

            return mapper(result);
        }
    }
}
