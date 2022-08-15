using FluentResults;
using SeaInk.Core.TableLayout.ComponentsBase;

namespace SeaInk.Core.TableLayout.Successes
{
    public class SuccessComponent : Success
    {
        public SuccessComponent(LayoutComponent component)
            : base($"Command successfully executed on component {component}")
        {
            Component = component;
        }

        public LayoutComponent Component { get; }
    }
}