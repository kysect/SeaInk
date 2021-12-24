using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.ComponentsBase;

namespace SeaInk.Core.TableLayout.CommandsBase
{
    public interface ILayoutCommand
    {
        Result Execute(LayoutComponent target, ISheetIndex begin, ISheetEditor? editor);
    }
}