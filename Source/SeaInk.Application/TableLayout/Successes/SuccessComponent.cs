using FluentResults;
using SeaInk.Application.TableLayout.ComponentsBase;

namespace SeaInk.Application.TableLayout.Successes
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