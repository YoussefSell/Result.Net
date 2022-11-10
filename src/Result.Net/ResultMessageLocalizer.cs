namespace ResultNet
{
    using System;

    /// <summary>
    /// the result message localizer
    /// </summary>
    public static class ResultMessageLocalizer
    {
        /// <summary>
        /// if set to true an exception of type <see cref="ResultMessageLocalizationNotFoundException"/>
        /// will be thrown in case we couldn't find a translation for a given text_code, usefully when debugging.
        /// </summary>
        public static bool ThrowIfNotFound = false;

        /// <summary>
        /// if set to true, null will be returned when no translation has been found for a given text_code.
        /// if not, the text_code value will be returned
        /// </summary>
        public static bool ReturnNull = false;

        /// <summary>
        /// Set the function that returns the localized string of a given text_code.
        /// as an argument the text code will be supplied, also the language code if supplied
        /// </summary>
        public static Func<string, string, string> GetText = null;

        /// <summary>
        /// get the localized message associated with the given text_code,
        /// the message will be loaded using the error code from <see cref="ResultMessageLocalizer.GetText"/>
        /// </summary>
        /// <param name="text_code">the text_code to use when loading the translation with <see cref="ResultMessageLocalizer.GetText"/></param>
        /// <param name="language_code">the language code if any, this value will be passed to <see cref="ResultMessageLocalizer.GetText"/></param>
        /// <returns>the current instance to enable method chaining</returns>
        /// <exception cref="ResultTextCodeNotSpecifiedException">if the text_code is not set</exception>
        /// <exception cref="LocalizationGetTextMethodNotImplementedException">if the <see cref="ResultMessageLocalizer.GetText"/> is not set</exception>
        /// <exception cref="ResultMessageLocalizationNotFoundException">if the localized message cannot be found</exception>
        public static string Localize(string text_code, string language_code = null)
        {
            if (string.IsNullOrEmpty(text_code))
                throw new ResultTextCodeNotSpecifiedException(ResultTextCodeNotSpecifiedException.Message2);

            if (GetText is null)
                throw new LocalizationGetTextMethodNotImplementedException();

            var message_text = GetText(text_code, language_code);
            if (!string.IsNullOrEmpty(message_text))
                return message_text;

            if (ThrowIfNotFound)
                throw new ResultMessageLocalizationNotFoundException(text_code);

            return ReturnNull ? null : text_code;
        }
    }
}
