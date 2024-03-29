using FluentResults;

namespace SeaInk.Core.TableLayout.Errors
{
    public class InvalidComponentError<T> : Error
    {
        public InvalidComponentError(T expected, T received)
            : base($"Received component {received} is not equal to expected component {expected}") { }
    }
}