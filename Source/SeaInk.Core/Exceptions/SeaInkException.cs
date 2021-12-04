using System;
using System.Runtime.Serialization;

namespace SeaInk.Core.Exceptions
{
    public class SeaInkException : Exception
    {
        public SeaInkException() { }

        public SeaInkException(string? message)
            : base(message) { }

        public SeaInkException(string? message, Exception? innerException)
            : base(message, innerException) { }

        protected SeaInkException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}