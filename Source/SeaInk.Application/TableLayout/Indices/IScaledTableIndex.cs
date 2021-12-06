using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Indices
{
    public interface IScaledTableIndex : ISheetIndex
    {
         Scale Scale { get; }
    }
}