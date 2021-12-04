using System;
using System.Runtime.Serialization;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    [Serializable]
    public class InvalidScaleComponentException : SeaInkException
    {
        public InvalidScaleComponentException(string argName, int value)
            : base($"{argName} must be positive. Value {value}") { }

        protected InvalidScaleComponentException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}