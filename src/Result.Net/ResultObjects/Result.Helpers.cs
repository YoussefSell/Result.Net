namespace Result.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// this class defines a result object without a data, 
    /// use it to describe the status of an operation with no return data.
    /// </summary>
    public partial class Result
    {
        /// <summary>
        /// create a new <see cref="Result"/> instance with failed status.
        /// </summary>
        /// <returns>an instance of <see cref="Result"/></returns>
        public static Result Failure() => new Result(ResultStatus.Failed);

        /// <summary>
        /// create a new <see cref="Result{TData}"/> instance with failed status.
        /// </summary>
        /// <typeparam name="TData">Type of the result data</typeparam>
        /// <returns>an instance of <see cref="Result{TData}"/></returns>
        public static Result<TData> Failure<TData>() => new Result<TData>(default, ResultStatus.Failed);

        /// <summary>
        /// create a new <see cref="ListResult{TData}"/> instance with failed status.
        /// </summary>
        /// <typeparam name="TData">Type of the result data</typeparam>
        /// <returns>an instance of <see cref="ListResult{TData}"/></returns>
        public static ListResult<TData> ListFailure<TData>() => new ListResult<TData>(default, ResultStatus.Failed);

        /// <summary>
        /// create a new <see cref="Result"/> instance with failed status.
        /// </summary>
        /// <returns>an instance of <see cref="Result"/></returns>
        public static Result Success() => new Result(ResultStatus.Succeed);

        /// <summary>
        /// create a new <see cref="Result{TData}"/> instance with success status and a data.
        /// </summary>
        /// <typeparam name="TData">Type of the result data</typeparam>
        /// <param name="data">the data associated with the result.</param>
        /// <returns>the <see cref="Result{TData}"/> instance</returns>
        public static Result<TData> Success<TData>(TData data) => new Result<TData>(data, ResultStatus.Succeed);

        /// <summary>
        /// create a new success list Result instance with the given data.
        /// </summary>
        /// <typeparam name="TData">Type of the result data</typeparam>
        /// <param name="data">the data associated with the result.</param>
        /// <returns>the <see cref="ListResult{TData}"/> instance</returns>
        public static ListResult<TData> ListSuccess<TData>(IReadOnlyCollection<TData> data) => new ListResult<TData>(data, ResultStatus.Succeed);

        /// <summary>
        /// create a fake Failed Result with a resultCode of <see cref="ResultCode.FakeError"/>
        /// </summary>
        /// <returns>instance of <see cref="Result"/></returns>
        public static Result FakeFailure() => Failure().WithMessage("this is a fake failure for testing purposes").WithCode(ResultCode.FakeError);

        /// <summary>
        /// create a fake Failed Result with a resultCode of <see cref="ResultCode.FakeError"/>
        /// </summary>
        /// <typeparam name="TData">Type of the result data</typeparam>
        /// <returns>instance of <see cref="Result{TData}"/></returns>
        public static Result<TData> FakeFailure<TData>() => Failure<TData>().WithMessage("this is a fake failure for testing purposes").WithCode(ResultCode.FakeError);

        /// <summary>
        /// create a fake Failed list Result with a resultCode of <see cref="ResultCode.FakeError"/>
        /// </summary>
        /// <typeparam name="TData">Type of the result data</typeparam>
        /// <returns>instance of <see cref="ListResult{TData}"/></returns>
        public static ListResult<TData> ListFakeFailure<TData>() => ListFailure<TData>().WithMessage("this is a fake failure for testing purposes").WithCode(ResultCode.FakeError);

        /// <summary>
        /// build a Result form the given result instance.
        /// </summary>
        /// <typeparam name="TData">the type of th result data</typeparam>
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
        /// <typeparam name="TData">the type of th result data</typeparam>
        /// <param name="result">the result instance to build the from.</param>
        /// <returns>the new instance of result object</returns>
        /// <exception cref="ArgumentNullException">if the given result instance is null</exception>
        public static TResultOut Clone<TData, TResultOut>(Result<TData> result)
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
        /// <typeparam name="TData">the type of th result data</typeparam>
        /// <param name="result">the result instance to build the from.</param>
        /// <returns>the new instance of result object</returns>
        /// <exception cref="ArgumentNullException">if the given result instance is null</exception>
        public static TResultOut Clone<TData, TResultOut>(ListResult<TData> result)
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

