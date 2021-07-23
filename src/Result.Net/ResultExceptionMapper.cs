namespace Result.Net
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// the result mapper to configure global mapping;
    /// </summary>
    public static class ResultExceptionMapper<TException>
        where TException : ResultException
    {
        private static readonly IDictionary<string, Func<Result, TException>> _exceptionMapping
            = new Dictionary<string, Func<Result, TException>>();

        /// <summary>
        /// add a new mapping of the given code to the setup function to create the exception from the result instance.
        /// </summary>
        /// <param name="code">the result error code</param>
        /// <param name="setup">the mapping setup function</param>
        public static void AddMapping(string code, Func<Result, TException> setup)
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
        public static Func<Result, TException> GetMapping(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            if (!_exceptionMapping.TryGetValue(code, out Func<Result, TException> setupFunc))
                return null;
            
            return setupFunc;
        }
    }
}
