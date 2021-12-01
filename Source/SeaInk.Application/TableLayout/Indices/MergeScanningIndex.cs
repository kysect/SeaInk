using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Indices
{
    public class MergeScanningIndex : ITableIndex
    {
        private readonly ITableIndex _index;
        private readonly ITableEditor _editor;
        private readonly Scale? _scale;

        public MergeScanningIndex(ITableIndex index, ITableEditor editor)
        {
            _index = index;
            _editor = editor;

            if (index is IScaledTableIndex scaledTableIndex)
                _scale = scaledTableIndex.Scale;
        }

        public int Column => _index.Column;
        public int Row => _index.Row;

        public void MoveHorizontally(int value = 1)
        {
            if (_scale is null)
            {
                _index.MoveHorizontally(value);
                return;
            }

            ITableIndex index = _index.Copy();
            _index.MoveHorizontally(value);
            _editor.EnqueueMerge(index, new Frame(_scale.Horizontal, _scale.Vertical));
        }

        public void MoveVertically(int value = 1)
        {
            if (_scale is null)
            {
                _index.MoveVertically(value);
                return;
            }

            ITableIndex index = _index.Copy();
            _index.MoveVertically(value);
            _editor.EnqueueMerge(index, new Frame(_scale.Horizontal, _scale.Vertical));
        }

        public ITableIndex Copy()
            => new MergeScanningIndex(_index.Copy(), _editor);

        public override string ToString()
            => _index.ToString() ?? string.Empty;
    }
}