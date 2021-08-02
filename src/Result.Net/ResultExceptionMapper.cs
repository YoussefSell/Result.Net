namespace ResultNet
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// the result mapper to configure global mapping;
    /// </summary>
    public static class ResultExceptionMapper
    {
        private static readonly IDictionary<string, Func<Result, object>> _exceptionMapping
            = new Dictionary<string, Func<Result, object>>();

        /// <summary>
        /// add a new mapping of the given code to the setup function to create the exception from the result instance.
        /// </summary>
        /// <param name="code">the result error code</param>
        /// <param name="setup">the mapping setup function</param>
        public static void AddMapping<TException>(string code, Func<Result, TException> setup) where TException : ResultException
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
        public static Func<Result, TException> GetMapping<TException>(string code) where TException : ResultException
        {
            if (string.IsNullOrEmpty(code))
                return null;

            if (!_exceptionMapping.TryGetValue(code, out Func<Result, object> setupFunc))
                return null;

            return setupFunc as Func<Result, TException>;
        }
    }
}
