using FluentResults;
using SeaInk.Application.TableLayout.ComponentsBase;

namespace SeaInk.Application.TableLayout.Errors
{
    public class NotContainedComponentError : Error
    {
        public NotContainedComponentError(LayoutComponent component)
            : base($"Component {component} is not contained in requested container") { }
    }
}