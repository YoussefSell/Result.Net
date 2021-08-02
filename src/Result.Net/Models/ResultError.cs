namespace ResultNet
{
    using System;
    using System.Linq;

    /// <summary>
    /// the result error details
    /// </summary>
    public partial class ResultError
    {
        /// <summary>
        /// create an instance of <see cref="ResultError"/>
        /// </summary>
        /// <param name="message">the message that describe the error</param>
        /// <param name="code">the code associated with the error</param>
        public ResultError(string message, string code) : this(message, code, string.Empty, string.Empty) { }

        /// <summary>
        /// create an instance of <see cref="ResultError"/>
        /// </summary>
        /// <param name="message">the message that describe the error</param>
        /// <param name="code">the code associated with the error</param>
        /// <param name="source">the source of the error</param>
        /// <param name="type">the type of the error</param>
        public ResultError(string message, string code, string source, string type)
        {
            Message = message;
            Code = code;
            Source = source;
            Type = type;
        }

        /// <summary>
        /// the error message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// the code associated with the exception
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// the source of the error
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// type of the error
        /// </summary>
        public string Type { get; }
    }

    /// <summary>
    /// partial part for <see cref="ResultError"/>
    /// </summary>
    public partial class ResultError : IEquatable<ResultError>
    {
        /// <inheritdoc/>
        public bool Equals(ResultError other)
        {
            if (other is null) return false;

            if (!other.Code.IsValid() && Code.IsValid()) return false;
            if (!other.Message.IsValid() && Message.IsValid()) return false;
            if (!other.Type.IsValid() && Type.IsValid()) return false;
            if (!other.Source.IsValid() && Source.IsValid()) return false;

            return other.Code.Equals(Code) &&
                other.Message.Equals(Message) &&
                other.Type.Equals(Type) &&
                other.Source.Equals(Source);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != typeof(ResultError)) return false;
            if (ReferenceEquals(obj, this)) return true;
            return Equals(obj as ResultError);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ (Code.IsValid() ? Code.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type.IsValid() ? Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Source.IsValid() ? Source.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Message.IsValid() ? Message.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"{Message} | Code: '{Code}', Source: '{Source}'";

        /// <summary>
        /// Generate an Error Object from an exception
        /// </summary>
        /// <param name="exception">the exception</param>
        /// <returns>an Error Object instance</returns>
        public static ResultError MapFromException(Exception exception)
            => new ResultError(
                source: exception.Source,
                message: exception.Message,
                type: exception.GetType().Name,
                code: ResultCode.OperationFailedException
            );

        /// <summary>
        /// extract list of errors from the given exception by looking inside the innerException
        /// </summary>
        /// <param name="exception">the exception to extract the errors from it.</param>
        /// <returns>an array of <see cref="ResultError"/></returns>
        public static ResultError[] GetFromException(Exception exception)
            => exception?.FromHierarchy(ex => ex.InnerException)
                .Select(ex => MapFromException(exception)).ToArray() ?? Array.Empty<ResultError>();
    }
}
