namespace ResultNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// exception type related to result object
    /// </summary>
    [Serializable]
    public class ResultException : Exception
    {
        /// <summary>
        /// the error code associated with this result exception
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// this list of errors associated with the result exception.
        /// </summary>
        public ICollection<ResultError> Errors { get; }

        /// <summary>
        /// a unique log trace code used to trace the result in logs
        /// </summary>
        public string LogTraceCode { get; }

        /// <summary>
        /// create an instance of <see cref="ResultException"/>.
        /// </summary>
        /// <param name="result">the result instance</param>
        public ResultException(Result result) : this(result, null) { }

        /// <summary>
        /// create an instance of <see cref="ResultException"/>.
        /// </summary>
        /// <param name="result">the result instance</param>
        /// <param name="inner">the inner exception if any</param>
        public ResultException(Result result, Exception inner) : base(result.Message, inner)
        {
            if (result.HasMetaData())
                foreach (var meta in result.MetaData)
                    Data.Add(meta.Key, meta.Value);

            Code = result.Code;
            Errors = result.Errors;
            LogTraceCode = result.LogTraceCode;
        }

        /// <inheritdoc/>
        protected ResultException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        /// <summary>
        /// convert the current exception to a result instance.
        /// </summary>
        /// <returns>an instance of Result object</returns>
        public Result ToResult()
        {
            var result = new Result(status: ResultStatus.Failed, Message, Code, LogTraceCode, Errors, null);

            if (Data.Count > 0)
                foreach (var key in Data.Keys)
                    result.WithMataData(key as string, Data[key]);

            return result;
        }

        /// <summary>
        /// execute a trow statement on the current exception instance.
        /// </summary>
        public void Throw() => throw this;
    }
}
