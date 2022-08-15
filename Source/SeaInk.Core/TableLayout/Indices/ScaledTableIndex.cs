using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout.Indices
{
    public readonly struct ScaledTableIndex : IScaledTableIndex
    {
        private readonly ISheetIndex _index;

        public ScaledTableIndex(Scale scale, ISheetIndex index)
        {
            Scale = scale;
            _index = index;
        }

        public Scale Scale { get; }

        public ColumnIndex Column => _index.Column;
        public RowIndex Row => _index.Row;
        public bool IsOpen => _index.IsOpen;

        ISheetIndex ISheetIndex.Copy()
            => _index.Copy();

        public ISheetIndex Add(ISheetIndex other)
            => _index.Add(new SheetIndex(other.Column.Value * Scale.Horizontal, other.Row.Value * Scale.Vertical));

        public ISheetIndex Subtract(ISheetIndex other)
            => _index.Subtract(new SheetIndex(other.Column.Value * Scale.Horizontal, other.Row.Value * Scale.Vertical));

        public bool IsEquallyOpen(ISheetIndex other)
            => _index.IsEquallyOpen(other);

        public ISheetIndex Copy()
            => new ScaledTableIndex(Scale, _index.Copy());

        public bool Equals(ISheetIndex? other)
            => _index.Equals(other);

        public override string ToString()
            => _index.ToString() ?? string.Empty;
    }
}