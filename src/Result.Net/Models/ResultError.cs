namespace ResultNet
{
    using System;
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

            Exception = default;
        }

        /// <summary>
        /// create an instance of the Result Error associated with a given exception
        /// </summary>
        /// <param name="exception">the exception instance</param>
        public ResultError(Exception exception)
        {
            Exception = exception;

            Source = exception.Source;
            Message = exception.Message;
            Code = ResultCode.OperationFailedException;
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
        /// Get the exception associated with this result error.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// is this error is associated with an exception or not
        /// </summary>
        public bool IsExceptionError => Exception is not null;
    }

    /// <summary>
    /// partial part for <see cref="ResultError"/>
    /// </summary>
    public partial struct ResultError : IEquatable<ResultError>
    {
        /// <inheritdoc/>
        public bool Equals(ResultError other)
        {
            if (other.IsExceptionError && IsExceptionError)
                return other.Exception.Equals(Exception);

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
                return Exception.GetHashCode();

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
    }
}
