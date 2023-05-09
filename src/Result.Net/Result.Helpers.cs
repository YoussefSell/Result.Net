namespace ResultNet
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
        public static Result Failure() => new Result(ResultStatus.Failed, string.Empty, string.Empty, Utilities.GenerateLogTraceErrorCode(), null, null);

        /// <summary>
        /// create a new <see cref="Result{TData}"/> instance with failed status.
        /// </summary>
        /// <typeparam name="TData">Type of the result data</typeparam>
        /// <returns>an instance of <see cref="Result{TData}"/></returns>
        public static Result<TData> Failure<TData>() => new Result<TData>(default, ResultStatus.Failed, string.Empty, string.Empty, Utilities.GenerateLogTraceErrorCode(), null, null);

        /// <summary>
        /// create a new <see cref="Result"/> instance with failed status.
        /// </summary>
        /// <returns>an instance of <see cref="Result"/></returns>
        public static Result Success() => new Result(ResultStatus.Succeed, string.Empty, string.Empty, null, null, null);

        /// <summary>
        /// create a new <see cref="Result{TData}"/> instance with success status and a data.
        /// </summary>
        /// <typeparam name="TData">Type of the result data</typeparam>
        /// <param name="data">the data associated with the result.</param>
        /// <returns>the <see cref="Result{TData}"/> instance</returns>
        public static Result<TData> Success<TData>(TData data) => new Result<TData>(data, ResultStatus.Succeed, string.Empty, string.Empty, null, null, null);

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
    }
}

