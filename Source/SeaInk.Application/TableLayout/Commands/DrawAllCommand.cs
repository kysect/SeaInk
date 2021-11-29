using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.Commands
{
    public class DrawAllCommand : ILayoutCommand
    {
        public bool TryExecute(LayoutComponent target, ITableIndex begin, ITableEditor? editor)
        {
            if (target is not IDrawableLayoutComponent drawableComponent)
                return false;

            target.TryExecuteCommand(new DrawComponentCommand(drawableComponent), begin, editor);
            return false;
        }
    }
}