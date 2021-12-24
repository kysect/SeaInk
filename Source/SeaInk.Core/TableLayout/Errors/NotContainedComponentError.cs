using FluentResults;
using SeaInk.Core.TableLayout.ComponentsBase;

namespace SeaInk.Core.TableLayout.Errors
{
    public class NotContainedComponentError : Error
    {
        public NotContainedComponentError(LayoutComponent component)
            : base($"Component {component} is not contained in requested container") { }
    }
}