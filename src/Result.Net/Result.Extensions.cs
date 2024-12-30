namespace ResultNet
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// a set of extensions methods on top of the result object
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// set the message related to this result instance
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <param name="message">the message data</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithMessage<TResult>(this TResult result, string message) where TResult : Result
        {
            result.Message = message;
            return result;
        }

        /// <summary>
        /// set the localized message related to this result instance,
        /// the message will be loaded using the error code from <see cref="ResultMessageLocalizer.GetText"/>,
        /// and using the error code of the result as the text_code
        /// </summary>
        /// <typeparam name="TResult">the result instance type</typeparam>
        /// <param name="result">the result object instance</param>
        /// <param name="language_code">the language code if any, this value will be passed to <see cref="ResultMessageLocalizer.GetText"/></param>
        /// <returns>the current instance to enable method chaining</returns>
        /// <exception cref="ResultTextCodeNotSpecifiedException">if the text_code is not set</exception>
        /// <exception cref="LocalizationGetTextMethodNotImplementedException">if the <see cref="ResultMessageLocalizer.GetText"/> is not set</exception>
        /// <exception cref="ResultMessageLocalizationNotFoundException">if the localized message cannot be found</exception>
        public static TResult WithLocalizedMessage<TResult>(this TResult result) where TResult : Result
        {
            if (string.IsNullOrEmpty(result.Code))
                throw new ResultTextCodeNotSpecifiedException(ResultTextCodeNotSpecifiedException.Message1);

            return result.WithLocalizedMessage(result.Code);
        }

        /// <summary>
        /// set the localized message related to this result instance,
        /// the message will be loaded using the error code from <see cref="ResultMessageLocalizer.GetText"/>
        /// </summary>
        /// <typeparam name="TResult">the result instance type</typeparam>
        /// <param name="result">the result object instance</param>
        /// <param name="text_code">the text_code to use when loading the translation with <see cref="ResultMessageLocalizer.GetText"/></param>
        /// <param name="language_code">the language code if any, this value will be passed to <see cref="ResultMessageLocalizer.GetText"/></param>
        /// <returns>the current instance to enable method chaining</returns>
        /// <exception cref="ResultTextCodeNotSpecifiedException">if the text_code is not set</exception>
        /// <exception cref="LocalizationGetTextMethodNotImplementedException">if the <see cref="ResultMessageLocalizer.GetText"/> is not set</exception>
        /// <exception cref="ResultMessageLocalizationNotFoundException">if the localized message cannot be found</exception>
        public static TResult WithLocalizedMessage<TResult>(this TResult result, string text_code) where TResult : Result 
            => result.WithMessage(ResultMessageLocalizer.Localize(text_code));

        /// <summary>
        /// set the code related to this result instance
        /// </summary>
        /// <param name="result">the result object instance</param>
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
        /// <param name="result">the result object instance</param>
        /// <param name="traceCode">the trace Code data</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithLogTraceCode<TResult>(this TResult result, string traceCode) where TResult : Result
        {
            result.LogTraceCode = traceCode;
            return result;
        }

        /// <summary>
        /// add a result error to the list of errors
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <param name="message">the error message</param>
        /// <param name="code">the error code</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithError<TResult>(this TResult result, string message, string code) where TResult : Result
            => WithError(result, new ResultError(message, code));

        /// <summary>
        /// add a result error to the list of errors
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <param name="message">the error message</param>
        /// <param name="code">the error code</param>
        /// <param name="source">the error source</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithError<TResult>(this TResult result, string message, string code, string source) where TResult : Result
            => WithError(result, new ResultError(message, code, source));

        /// <summary>
        /// add a result error to the list of errors
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <param name="error">the error instance</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithError<TResult>(this TResult result, ResultError error) where TResult : Result
        {
            result.Errors.Add(error);
            return result;
        }

        /// <summary>
        /// set the exception related to this result instance
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <param name="exception">the exception instance</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithError<TResult>(this TResult result, Exception exception) where TResult : Result
        {
            if (exception is null)
                return result;

            result.Errors.Add(new ResultError(exception));
            return result;
        }

        /// <summary>
        /// add a result error to the list of errors
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <param name="errors">the errors</param>
        /// <returns>the current instance to enable method chaining</returns>
        public static TResult WithErrors<TResult>(this TResult result, params ResultError[] errors) where TResult : Result
        {
            foreach (var error in errors)
                result.Errors.Add(error);

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
        public static TResult WithMataData<TResult>(this TResult result, string key, object data) where TResult : Result
        {
            result.MetaData.Add(key, data);
            return result;
        }

        /// <summary>
        /// adds an element with the provided key and data to the data list of the result instance
        /// </summary>
        /// <param name="error">the result object instance</param>
        /// <param name="key">the key of the data</param>
        /// <param name="data">the data instance</param>
        /// <returns>the instance of result to enable method chaining</returns>
        public static ResultError WithMataData(this ResultError error, string key, object data)
        {
            error.MetaData.Add(key, data);
            return error;
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
        /// Determines whether the Result object instance represent a successful result.
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <returns>true if failure result, otherwise false</returns>
        public static bool IsFailure(this Result result)
            => !(result is null) && result.Status == ResultStatus.Failed;

        /// <summary>
        /// Determines whether the failure is caused by the given code.
        /// </summary>
        /// <remarks>first we will check the <see cref="Result.Code"/> if equals the given code, 
        /// if not and <paramref name="checkErrors"/> is set to true will check the <see cref="Result.Errors"/> is any has the given error code</remarks>
        /// <param name="result">the result object instance</param>
        /// <param name="code">the error code to check for.</param>
        /// <param name="checkErrors">true to check any of the errors of the result object has the given code, if false only the result instance will be checked, defaulted to true.</param>
        /// <returns>true if failure caused by the given code, otherwise false</returns>
        public static bool FailedBecause(this Result result, string code, bool checkErrors = true)
        {
            if (result is null)
                return false;

            if (result.Code == code)
                return true;

            if (checkErrors && !(result.Errors is null))
                return result.Errors.Any(error => error.Code == code);

            return false;
        }

        /// <summary>
        /// run the given action base on result if success or failure
        /// </summary>
        /// <typeparam name="TResult">the type of the result</typeparam>
        /// <param name="result">the result instance</param>
        /// <param name="onSuccess">the action to run on success</param>
        /// <param name="onFailure">the action to run on failure</param>
        public static void Match<TResult>(this TResult result, Action<TResult> onSuccess, Action<TResult> onFailure)
            where TResult : Result
        {
            if (result.IsFailure())
            {
                onFailure?.Invoke(result);
                return;
            }

            onSuccess?.Invoke(result);
        }

        /// <summary>
        /// match the result status and run the function accordingly.
        /// </summary>
        /// <typeparam name="TResult">the type of the result</typeparam>
        /// <typeparam name="TOut">the type of the output</typeparam>
        /// <param name="result">the result instance</param>
        /// <param name="onSuccess">the action to run on success</param>
        /// <param name="onFailure">the action to run on failure</param>
        /// <returns>the output </returns>
        public static TOut Match<TResult, TOut>(this TResult result, Func<TResult, TOut> onSuccess, Func<TResult, TOut> onFailure)
            where TResult : Result
        {
            if (result.IsFailure())
            {
                if (onFailure is null)
                    return default;

                return onFailure(result);
            }

            if (onSuccess is null)
                return default;

            return onSuccess(result);
        }

        /// <summary>
        /// convert the result instance to an exception.
        /// </summary>
        /// <typeparam name="TResult">the result type</typeparam>
        /// <param name="result">the result object instance</param>
        /// <returns>an instance of the exception.</returns>
        public static ResultException ToException<TResult>(this TResult result)
            where TResult : Result
        {
            var mapper = ResultExceptionMapper.GetMapping<ResultException>(result.Code);
            if (mapper is null)
                return new ResultException(result);

            return mapper(result);
        }

        /// <summary>
        /// convert the result instance to an exception.
        /// </summary>
        /// <typeparam name="TException">the exception type</typeparam>
        /// <param name="result">the result object instance</param>
        /// <returns>an instance of the exception.</returns>
        /// <exception cref="ResultExceptionMappingNotFoundException">if there is no mapping configured for the result error code to the given exception type.</exception>
        public static TException ToException<TException>(this Result result)
            where TException : ResultException
        {
            var mapper = ResultExceptionMapper.GetMapping<TException>(result.Code);
            if (mapper is null)
                throw new ResultExceptionMappingNotFoundException(result.Code, typeof(TException));

            return mapper(result);
        }

        /// <summary>
        /// convert the current exception to a result instance.
        /// </summary>
        /// <returns>an instance of Result object</returns>
        public static Result ToResult(this Exception exception)
        {
            var result = Result.Failure()
                .WithMessage(exception.Message)
                .WithCode(ResultCode.OperationFailedException)
                .WithError(exception);

            return result;
        }

        /// <summary>
        /// execute a trow statement on the current exception instance.
        /// </summary>
        /// <param name="exception">the exception instance.</param>
        public static void Throw(this Exception exception) => throw exception;

        /// <summary>
        /// cast the exception to the given type.
        /// </summary>
        /// <typeparam name="TOut">the type to cast the exception to it</typeparam>
        /// <param name="exception">the exception instance.</param>
        public static TOut As<TOut>(this ResultException exception) where TOut : ResultException
            => (TOut)exception;

        /// <summary>
        /// cast the given result instance to the given out type
        /// </summary>
        /// <typeparam name="TOut">the result output type</typeparam>
        /// <param name="result">the result instance</param>
        /// <returns>the new result type</returns>
        public static Result<TOut> Cast<TOut>(this Result result)
        {
            var instance = new Result<TOut>(default!, result.Status, result.Message, result.Code, result.LogTraceCode, new HashSet<ResultError>(), new Dictionary<string, object>());

            foreach (var item in result.MetaData)
                instance.MetaData.Add(item.Key, item.Value);

            foreach (var item in result.Errors)
                instance.Errors.Add(item);

            return instance;
        }
    }
}
