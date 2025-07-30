namespace ResultNet
{
    using System;

    /// <summary>
    /// exception thrown when no translation has been found for a given text_code
    /// </summary>
    [Serializable]
    public class ResultTextCodeNotSpecifiedException : ResultException
    {
        internal static readonly string Message1 = @"there is no error code set on this result object instance, make sure you are calling WithLocalizedMessage() after you called WithCode(), or verify the value of the error code that is not null or empty";
        internal static readonly string Message2 = @"the given text_code is value is null or empty";

        /// <summary>
        /// create an instance of <see cref="ResultTextCodeNotSpecifiedException"/>
        /// </summary>
        /// <param name="message">the exception message</param>
        public ResultTextCodeNotSpecifiedException(string message)
            : base(Result.Failure()
                  .WithMessage(message)
                  .WithCode("result_text_code_not_specified")) {}

        /// <inheritdoc/>
        protected ResultTextCodeNotSpecifiedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
