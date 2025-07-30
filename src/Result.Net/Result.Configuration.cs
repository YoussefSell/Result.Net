namespace ResultNet
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// the configuration class for the result object
    /// </summary>
    public class ResultObjectConfiguration
    {
        public readonly static ResultObjectConfiguration Default = new();

        /// <summary>
        /// Get or set the result message localization settings.
        /// </summary>
        public MessageLocalizer Localization { get; set; } = new();

        /// <summary>
        /// Get or set the exception mapper to configure global exceptions mapping;
        /// </summary>
        public ResultExceptionMapper ExceptionMapper { get; set; } = new();

        internal static void Initialize(Action<ResultObjectConfiguration> setup)
        {
            setup?.Invoke(Default);
        }

        /// <summary>
        /// the result message localizer
        /// </summary>
        public class MessageLocalizer
        {
            /// <summary>
            /// if set to true an exception of type <see cref="ResultMessageLocalizationNotFoundException"/>
            /// will be thrown in case we couldn't find a translation for a given text_code, usefully when debugging.
            /// </summary>
            public bool ThrowIfNotFound { get; set; } = false;

            /// <summary>
            /// if set to true, null will be returned when no translation has been found for a given text_code.
            /// if not, the text_code value will be returned
            /// </summary>
            public bool ReturnNull { get; set; } = false;

            /// <summary>
            /// Set the function that returns the localized string of a given text_code.
            /// as an argument the text code will be supplied, also the language code if supplied
            /// </summary>
            public Func<string, string, string> GetText { get; set; } = null;

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

                if (Default.Localization.GetText is null)
                    throw new LocalizationGetTextMethodNotImplementedException();

                var message_text = Default.Localization.GetText(text_code, language_code);
                if (!string.IsNullOrEmpty(message_text))
                    return message_text;

                if (Default.Localization.ThrowIfNotFound)
                    throw new ResultMessageLocalizationNotFoundException(text_code);

                return Default.Localization.ReturnNull ? null : text_code;
            }
        }

        /// <summary>
        /// the exception mapper to configure global mapping;
        /// </summary>
        public class ResultExceptionMapper
        {
            private readonly IDictionary<string, Func<Result, object>> _exceptionMapping
                = new Dictionary<string, Func<Result, object>>();

            /// <summary>
            /// add a new mapping of the given code to the setup function to create the exception from the result instance.
            /// </summary>
            /// <param name="code">the result error code</param>
            /// <param name="setup">the mapping setup function</param>
            public void AddMapping<TException>(string code, Func<Result, TException> setup) where TException : ResultException
            {
                if (_exceptionMapping.ContainsKey(code))
                {
                    _exceptionMapping[code] = setup;
                    return;
                }

                _exceptionMapping.Add(code, setup);
            }

            /// <summary>
            /// get the mapping setup function for the given result error code.
            /// </summary>
            /// <param name="code">the error result code.</param>
            /// <returns>the mapping setup function</returns>
            public Func<Result, TException> GetMapping<TException>(string code) where TException : ResultException
            {
                if (string.IsNullOrEmpty(code))
                    return null;

                if (!_exceptionMapping.TryGetValue(code, out Func<Result, object> setupFunc))
                    return null;

                return setupFunc as Func<Result, TException>;
            }
        }
    }
}
