using FluentResults;
using SeaInk.Application.TableLayout.ComponentsBase;

namespace SeaInk.Application.TableLayout.Errors
{
    public class ComponentShouldBeIgnoredError : Error
    {
        public ComponentShouldBeIgnoredError(LayoutComponent component)
            : base($"Component {component} should be ignored") { }
    }
}