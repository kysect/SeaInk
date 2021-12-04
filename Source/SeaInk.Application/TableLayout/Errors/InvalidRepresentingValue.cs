using FluentResults;

namespace SeaInk.Application.TableLayout.Errors
{
    public class InvalidRepresentingValue<T> : Error
    {
        public InvalidRepresentingValue(T expected, T received)
            : base($"Received represented value {received} not equal to expected value {expected}") { }
    }
}