using FluentResults;
using SeaInk.Core.TableLayout.ComponentsBase;

namespace SeaInk.Core.TableLayout.Errors
{
    public class ComponentShouldBeIgnoredError : Error
    {
        public ComponentShouldBeIgnoredError(LayoutComponent component)
            : base($"Component {component} should be ignored") { }
    }
}