using FluentResults;
using SeaInk.Core.TableLayout.ComponentsBase;

namespace SeaInk.Core.TableLayout.Errors
{
    public class InvalidCommandHandlerError<T> : Error
    {
        public InvalidCommandHandlerError(LayoutComponent component)
            : base($"Passed component {component} is not of type {typeof(T)}") { }
    }
}