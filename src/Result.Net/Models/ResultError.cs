namespace ResultNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// the result error details
    /// </summary>
    public partial struct ResultError
    {
        /// <summary>
        /// create an instance of <see cref="ResultError"/>
        /// </summary>
        /// <param name="message">the message that describe the error</param>
        /// <param name="code">the code associated with the error</param>
        public ResultError(string message, string code) : this(message, code, string.Empty) { }

        /// <summary>
        /// create an instance of <see cref="ResultError"/>
        /// </summary>
        /// <param name="message">the message that describe the error</param>
        /// <param name="code">the code associated with the error</param>
        /// <param name="source">the source of the error</param>
        public ResultError(string message, string code, string source)
        {
            Code = code;
            Source = source;
            Message = message;
            IsExceptionError = false;
            MetaData = new Dictionary<string, object>();
        }

        /// <summary>
        /// create an instance of the Result Error associated with a given exception
        /// </summary>
        /// <param name="exception">the exception instance</param>
        public ResultError(Exception exception)
        {
            IsExceptionError = true;
            Source = exception.Source;
            Message = exception.Message;
            Code = ResultCode.OperationFailedException;
            MetaData = new Dictionary<string, object>() { { "original_exception", exception } }; 
        }

        /// <summary>
        /// Get the error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Get the code associated with the exception.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Get the source of the error.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// is this error is associated with an exception or not
        /// </summary>
        public bool IsExceptionError { get; }

        /// <summary>
        /// collection of meta-data that as key-value that contains additional info about the error.
        /// </summary>
        public Dictionary<string, object> MetaData { get; }
    }

    /// <summary>
    /// partial part for <see cref="ResultError"/>
    /// </summary>
    public partial struct ResultError : IEquatable<ResultError>
    {
        private const string EXCEPTION_KEY = "original_exception";

        /// <inheritdoc/>
        public bool Equals(ResultError other)
        {
            if (other.IsExceptionError && IsExceptionError)
                return other.GetException().Equals(GetException());

            if ((other.IsExceptionError && !IsExceptionError) || (!other.IsExceptionError && IsExceptionError))
                return false;

            if (!other.Code.IsValid() && Code.IsValid()) return false;
            if (!other.Message.IsValid() && Message.IsValid()) return false;
            if (!other.Source.IsValid() && Source.IsValid()) return false;

            return other.Code.Equals(Code) &&
                other.Message.Equals(Message) &&
                other.Source.Equals(Source);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is ResultError error) return Equals(error);
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            if (IsExceptionError)
                return GetException().GetHashCode();

            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ (Code.IsValid() ? Code.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Source.IsValid() ? Source.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Message.IsValid() ? Message.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"{Message} | Code: '{Code}', Source: '{Source}'";

        /// <summary>
        /// get the exception associated with this error if any
        /// </summary>
        /// <returns>the exception instance</returns>
        public Exception GetException() 
            => MetaData.TryGetValue(EXCEPTION_KEY, out var exception)
                ? exception as Exception : null;
    }
}
