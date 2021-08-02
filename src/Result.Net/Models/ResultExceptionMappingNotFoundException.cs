namespace ResultNet
{
    using System;

    /// <summary>
    /// exception thrown when no exception to result error code mapping has been found
    /// </summary>
    public class ResultExceptionMappingNotFoundException : ResultException
    {
        private static readonly string message = @"we couldn't find any mapping between the error code [{error_code}] and exception of type [{exception_name}].
make sure you have added the mapping of the error code to ResultExceptionMapper.
ex: ResultExceptionMapper.AddMapping(""{error_code}"", result => new {exception_name}(result));";

        /// <summary>
        /// create an instance of <see cref="ResultExceptionMappingNotFoundException"/>
        /// </summary>
        /// <param name="errorCode">the error code that the mapping is not configured for</param>
        /// <param name="exceptionType">the type of the exception with missing mapping configuration</param>
        public ResultExceptionMappingNotFoundException(string errorCode, Type exceptionType)
            : base(Result.Failure()
                  .WithMessage(message.Replace("{error_code}", errorCode).Replace("{exception_name}", exceptionType.FullName))
                  .WithCode("exception_mapping_not_found")) 
        {
            Data.Add("errorCodeMissingMapping", errorCode);
            Data.Add("exceptionMissingMapping", exceptionType.FullName);
        }
    }
}
