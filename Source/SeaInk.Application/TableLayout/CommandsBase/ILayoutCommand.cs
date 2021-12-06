using FluentResults;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.ComponentsBase;
using SeaInk.Application.TableLayout.Indices;

namespace SeaInk.Application.TableLayout.CommandsBase
{
    public interface ILayoutCommand
    {
        Result Execute(LayoutComponent target, ISheetIndex begin, ITableEditor? editor);
    }
}