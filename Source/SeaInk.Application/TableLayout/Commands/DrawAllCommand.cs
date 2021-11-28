using SeaInk.Application.TableLayout.CommandInterfaces;
using SeaInk.Application.TableLayout.CommandsBase;
using SeaInk.Application.TableLayout.Indices;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Commands
{
    public class DrawAllCommand : GenericLayoutCommand<IDrawableLayoutComponent>
    {
        protected override bool TryExecute(IDrawableLayoutComponent target, ITableIndex begin, ITableEditor? editor)
        {
            var index = new MergeScanningIndex(begin.Copy(), editor.ThrowIfNull(nameof(editor)));
            target.Draw(index, editor!);
            return false;
        }
    }
}