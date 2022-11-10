namespace ResultNet
{
    using System;

    /// <summary>
    /// exception thrown when no translation has been found for a given text_code
    /// </summary>
    [Serializable]
    public class ResultMessageLocalizationNotFoundException : ResultException
    {
        private static readonly string message = @"the following text code [{text_code}] is missing localization. make sure you have added a localization for [{text_code}] in your dictionary";

        /// <summary>
        /// create an instance of <see cref="ResultMessageLocalizationNotFoundException"/>
        /// </summary>
        /// <param name="textCode">the text code that we couldn't find the localization for</param>
        public ResultMessageLocalizationNotFoundException(string textCode)
            : base(Result.Failure()
                  .WithMessage(message.Replace("{text_code}", textCode))
                  .WithCode("text_code_localization_not_found")) 
        {
            Data.Add("textCodeMissingLocalization", textCode);
        }

        /// <inheritdoc/>
        protected ResultMessageLocalizationNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
