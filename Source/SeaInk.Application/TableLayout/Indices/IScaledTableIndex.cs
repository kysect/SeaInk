using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Indices
{
    public interface IScaledTableIndex : ITableIndex
    {
         Scale Scale { get; }
    }
}