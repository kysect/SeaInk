using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class InvalidScaleComponentException : SeaInkException
    {
        public InvalidScaleComponentException(string argName, int value)
            : base($"{argName} must be positive. Value {value}") { }
    }
}