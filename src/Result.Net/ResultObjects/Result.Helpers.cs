namespace Result.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// this class defines a result object without a value, 
    /// use it to describe the status of an operation with no return value.
    /// </summary>
    public partial class Result
    {
        /// <summary>
        /// create a new failed result instance.
        /// </summary>
        /// <returns>an instance of <see cref="Result"/></returns>
        public static Result Failed() => new Result(ResultStatus.Failed);

        /// <summary>
        /// create a new failed result instance.
        /// </summary>
        /// <typeparam name="TValue">Type of the result value</typeparam>
        /// <returns>an instance of <see cref="Result{TValue}"/></returns>
        public static Result<TValue> Failed<TValue>() => new Result<TValue>(default, ResultStatus.Failed);

        /// <summary>
        /// create a new failed list result instance.
        /// </summary>
        /// <typeparam name="TValue">Type of the result value</typeparam>
        /// <returns>an instance of <see cref="ListResult{TValue}"/></returns>
        public static ListResult<TValue> ListFailed<TValue>() => new ListResult<TValue>(default, ResultStatus.Failed);

        /// <summary>
        /// create a new failed paged result instance.
        /// </summary>
        /// <typeparam name="TValue">Type of the result value</typeparam>
        /// <returns>an instance of <see cref="PagedResult{TValue}"/></returns>
        public static PagedResult<TValue> PagedFailed<TValue>()
            => new PagedResult<TValue>(default, ResultStatus.Failed)
            {
                TotalRows = 0,
                PageSize = 10,
                PageIndex = 1,
            };

        /// <summary>
        /// create a new Success Result instance.
        /// </summary>
        /// <returns>an instance of <see cref="Result"/></returns>
        public static Result Success() => new Result(ResultStatus.Succeed);

        /// <summary>
        /// create a new Success Result instance with the given value.
        /// </summary>
        /// <typeparam name="TValue">Type of the result value</typeparam>
        /// <param name="value">the value associated with the result.</param>
        /// <returns>the <see cref="Result{TValue}"/> instance</returns>
        public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, ResultStatus.Succeed);

        /// <summary>
        /// create a new Success list Result instance with the given value.
        /// </summary>
        /// <typeparam name="TValue">Type of the result value</typeparam>
        /// <param name="value">the value associated with the result.</param>
        /// <returns>the <see cref="ListResult{TValue}"/> instance</returns>
        public static ListResult<TValue> ListSuccess<TValue>(IReadOnlyCollection<TValue> value) => new ListResult<TValue>(value, ResultStatus.Succeed);

        /// <summary>
        /// create a new Success paged Result instance with the given value.
        /// </summary>
        /// <param name="value">the value associated with the result.</param>
        /// <param name="pageIndex">the page index.</param>
        /// <param name="pageSize">the page size.</param>
        /// <param name="rowsCount">the total count of rows.</param>
        /// <returns>the <see cref="ListResult{TValue}"/> instance</returns>
        public static PagedResult<TValue> PagedSuccess<TValue>(IReadOnlyCollection<TValue> value, int pageIndex, int pageSize, int rowsCount)
            => new PagedResult<TValue>(value, ResultStatus.Succeed)
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalRows = rowsCount,
            };

        /// <summary>
        /// create a fake Failed Result with a resultCode of <see cref="ResultCode.FakeError"/>
        /// </summary>
        /// <returns>instance of <see cref="Result"/></returns>
        public static Result FakeFailure() => Failed().WithMessage("this is a fake failure for testing purposes").WithCode(ResultCode.FakeError);

        /// <summary>
        /// create a fake Failed Result with a resultCode of <see cref="ResultCode.FakeError"/>
        /// </summary>
        /// <typeparam name="TValue">Type of the result value</typeparam>
        /// <returns>instance of <see cref="Result{TValue}"/></returns>
        public static Result<TValue> FakeFailure<TValue>() => Failed<TValue>().WithMessage("this is a fake failure for testing purposes").WithCode(ResultCode.FakeError);

        /// <summary>
        /// create a fake Failed list Result with a resultCode of <see cref="ResultCode.FakeError"/>
        /// </summary>
        /// <typeparam name="TValue">Type of the result value</typeparam>
        /// <returns>instance of <see cref="ListResult{TValue}"/></returns>
        public static ListResult<TValue> ListFakeFailure<TValue>() => ListFailed<TValue>().WithMessage("this is a fake failure for testing purposes").WithCode(ResultCode.FakeError);

        /// <summary>
        /// create a fake Failed paged Result with a resultCode of <see cref="ResultCode.FakeError"/>
        /// </summary>
        /// <typeparam name="TValue">Type of the result value</typeparam>
        /// <returns>instance of <see cref="PagedResult{TValue}"/></returns>
        public static PagedResult<TValue> PagedFakeFailure<TValue>() => PagedFailed<TValue>().WithMessage("this is a fake failure for testing purposes").WithCode(ResultCode.FakeError);

        /// <summary>
        /// build a Result form the given result instance.
        /// </summary>
        /// <typeparam name="TValue">the type of th result value</typeparam>
        /// <param name="result">the result instance to build the from.</param>
        /// <returns>the new instance of result object</returns>
        /// <exception cref="ArgumentNullException">if the given result instance is null</exception>
        public static TResultOut Clone<TResultOut>(Result result) where TResultOut : Result
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            return new Result(result.Status)
                .WithMessage(result.Message)
                .WithCode(result.Code)
                .WithLogTraceCode(result.LogTraceCode)
                .WithErrors(result.Errors.ToArray()) as TResultOut;
        }

        /// <summary>
        /// build a Result form the given result instance.
        /// </summary>
        /// <typeparam name="TValue">the type of th result value</typeparam>
        /// <param name="result">the result instance to build the from.</param>
        /// <returns>the new instance of result object</returns>
        /// <exception cref="ArgumentNullException">if the given result instance is null</exception>
        public static TResultOut Clone<TValue, TResultOut>(Result<TValue> result)
            where TResultOut : Result
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            return new Result(result.Status)
                .WithMessage(result.Message)
                .WithCode(result.Code)
                .WithLogTraceCode(result.LogTraceCode)
                .WithErrors(result.Errors.ToArray()) as TResultOut;
        }

        /// <summary>
        /// build a Result form the given result instance.
        /// </summary>
        /// <typeparam name="TValue">the type of th result value</typeparam>
        /// <param name="result">the result instance to build the from.</param>
        /// <returns>the new instance of result object</returns>
        /// <exception cref="ArgumentNullException">if the given result instance is null</exception>
        public static TResultOut Clone<TValue, TResultOut>(ListResult<TValue> result)
            where TResultOut : Result
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            return new Result(result.Status)
                .WithMessage(result.Message)
                .WithCode(result.Code)
                .WithLogTraceCode(result.LogTraceCode)
                .WithErrors(result.Errors.ToArray()) as TResultOut;
        }

        /// <summary>
        /// build a Result form the given result instance.
        /// </summary>
        /// <typeparam name="TValue">the type of th result value</typeparam>
        /// <param name="result">the result instance to build the from.</param>
        /// <returns>the new instance of result object</returns>
        /// <exception cref="ArgumentNullException">if the given result instance is null</exception>
        public static TResultOut Clone<TValue, TResultOut>(PagedResult<TValue> result)
            where TResultOut : Result
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            return new Result(result.Status)
                .WithMessage(result.Message)
                .WithCode(result.Code)
                .WithLogTraceCode(result.LogTraceCode)
                .WithErrors(result.Errors.ToArray()) as TResultOut;
        }
    }
}

