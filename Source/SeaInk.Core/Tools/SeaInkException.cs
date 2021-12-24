using System;

namespace SeaInk.Core.Tools
{
    public abstract class SeaInkException : Exception
    {
        protected SeaInkException() { }

        protected SeaInkException(string? message)
            : base(message) { }

        protected SeaInkException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}