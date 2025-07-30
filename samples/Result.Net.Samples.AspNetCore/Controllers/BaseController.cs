using Microsoft.AspNetCore.Mvc;
using ResultNet;

namespace Result.Net.Samples.AspNetCore.Controllers
{
    /// <summary>
    /// the base controller
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// the server url
        /// </summary>
        public string BaseUrl => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        /// <summary>
        /// return an instance of <see cref="Result"/> as an Action result
        /// </summary>
        /// <returns>instance of action result</returns>
        protected static ActionResult<ResultNet.Result> SuccessResult()
            => ResultNet.Result.Success();

        /// <summary>
        /// return an instance of <see cref="Result{TData}"/> as an Action result
        /// </summary>
        /// <typeparam name="TOut">the type of the value</typeparam>
        /// <param name="value">the value instance</param>
        /// <returns>instance of action result</returns>
        protected static ActionResult<Result<TOut>> SuccessResult<TOut>(TOut value)
            => ResultNet.Result.Success(value);

        /// <summary>
        /// map the given result object into a action result instance
        /// </summary>
        /// <param name="result">the result object instance</param>
        /// <returns>an instance of the ActionResult</returns>
        protected ActionResult ActionResultFor<TResult>(TResult result) where TResult : ResultNet.Result
        {
            // the operation has failed
            if (result.Status == ResultStatus.Failed)
            {
                // result not found
                if (result.FailedBecause(ResultCode.NotFound))
                    return StatusCode(StatusCodes.Status404NotFound, result);

                // validation failed
                if (result.FailedBecause(ResultCode.ValidationFailed))
                    return StatusCode(StatusCodes.Status400BadRequest, result);

                // user is not authorized
                if (result.FailedBecause(ResultCode.Unauthorized))
                    return StatusCode(StatusCodes.Status401Unauthorized, result);

                if (result.FailedBecause(ResultCode.Forbidden))
                    return StatusCode(StatusCodes.Status403Forbidden, result);

                // something went wrong (exception)
                if (result.HasErrors())
                    return StatusCode(500, result);

                // if none of the above, then bad request
                return BadRequest(result);
            }

            // all set, return the operation result
            return Ok(result);
        }
    }
}
