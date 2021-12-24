using System;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Core.TableLayout.Models;

namespace SeaInk.Core.TableLayout.Indices
{
    public readonly struct MergeScanningIndex : ISheetIndex
    {
        private readonly ISheetIndex _index;
        private readonly ISheetEditor _editor;
        private readonly Scale? _scale;

        public MergeScanningIndex(ISheetIndex index, ISheetEditor editor)
        {
            _index = index;
            _editor = editor;

            _scale = index switch
            {
                IScaledTableIndex scaledTableIndex => scaledTableIndex.Scale,
                _ => null,
            };
        }

        public ColumnIndex Column => _index.Column;
        public RowIndex Row => _index.Row;
        public bool IsOpen => _index.IsOpen;

        public ISheetIndex Add(ISheetIndex other)
        {
            Merge(other, (i, ii) => i + ii);
            return _index.Add(other);
        }

        public ISheetIndex Subtract(ISheetIndex other)
        {
            Merge(other, (i, ii) => i - ii);
            return _index.Subtract(other);
        }

        public bool IsEquallyOpen(ISheetIndex other)
            => _index.IsEquallyOpen(other);

        public ISheetIndex Copy()
            => new MergeScanningIndex(_index.Copy(), _editor);

        public bool Equals(ISheetIndex? other)
            => _index.Equals(other);

        public override string ToString()
            => _index.ToString() ?? string.Empty;

        private void Merge(ISheetIndex other, Func<ISheetIndex, ISheetIndex, ISheetIndex> move)
        {
            if (_scale is null)
            {
                return;
            }

            var frame = new Frame(_scale.Horizontal, _scale.Vertical);

            for (int i = 0; i < other.Row; ++i)
            {
                ISheetIndex index = move.Invoke(_index.Copy(), new SheetIndex(0, i * _scale.Vertical));

                for (int j = 0; j < other.Column; ++j)
                {
                    _editor.EnqueueMerge(index, frame);
                    index = move.Invoke(index, new SheetIndex(_scale.Horizontal, 0));
                }
            }
        }
    }
}