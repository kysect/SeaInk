using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.CommandInterfaces;
using SeaInk.Core.TableLayout.CommandsBase;
using SeaInk.Core.TableLayout.Indices;
using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Commands
{
    public class DrawComponentCommand : GenericLayoutCommand<IDrawableLayoutComponent>
    {
        protected override Result Execute(IDrawableLayoutComponent target, ISheetIndex begin, ITableEditor? editor)
        {
            var index = new MergeScanningIndex(begin.Copy(), editor.ThrowIfNull());
            target.Draw(index, editor!);
            return Result.Ok();
        }
    }
}