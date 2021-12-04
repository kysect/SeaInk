using FluentResults;
using SeaInk.Application.TableLayout.ComponentsBase;

namespace SeaInk.Application.TableLayout.Errors
{
    public class InvalidCommandHandlerError<T> : Error
    {
        public InvalidCommandHandlerError(LayoutComponent component)
            : base($"Passed component {component} is not of type {typeof(T)}") { }
    }
}