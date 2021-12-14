using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout.Indices
{
    public interface IScaledTableIndex : ISheetIndex
    {
         Scale Scale { get; }
    }
}