using SeaInk.Core.Tools;

namespace SeaInk.Core.TableLayout.Exceptions
{
    public class InvalidScaleComponentException : SeaInkException
    {
        public InvalidScaleComponentException(string argName, int value)
            : base($"{argName} must be positive. Value {value}") { }
    }
}