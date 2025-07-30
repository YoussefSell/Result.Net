namespace ResultNet
{
    using System;

    /// <summary>
    /// exception thrown when <see cref="ResultMessageLocalizer.GetText"/> is not initialized
    /// </summary>
    [Serializable]
    public class LocalizationGetTextMethodNotImplementedException : ResultException
    {
        private static readonly string message = @$"the method [ResultMessageLocalizer.GetText] is not set, make sure you have initialized it, if not, remove the call to WithLocalizedMessage(), and use WithMessage() instead.";

        /// <summary>
        /// create an instance of <see cref="LocalizationGetTextMethodNotImplementedException"/>
        /// </summary>
        public LocalizationGetTextMethodNotImplementedException()
            : base(Result.Failure()
                  .WithMessage(message)
                  .WithCode("localization_get_text_method_not_implemented")) { }

        /// <inheritdoc/>
        protected LocalizationGetTextMethodNotImplementedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
