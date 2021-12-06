using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.ComponentsBase;

namespace SeaInk.Application.TableLayout.CommandsBase
{
    public interface ILayoutCommand
    {
        Result Execute(LayoutComponent target, ISheetIndex begin, ITableEditor? editor);
    }
}