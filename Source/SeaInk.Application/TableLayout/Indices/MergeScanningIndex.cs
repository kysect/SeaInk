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

        public void MoveHorizontally(int i = 1)
        {
            if (_scale is null)
            {
                _index.MoveHorizontally(i);
                return;
            }

            ITableIndex index = _index.Copy();
            _index.MoveHorizontally(i);
            _editor.EnqueueMerge(index, new Frame(_scale.Horizontal, _scale.Vertical));
        }

        public void MoveVertically(int i = 1)
        {
            if (_scale is null)
            {
                _index.MoveVertically(i);
                return;
            }

            ITableIndex index = _index.Copy();
            _index.MoveVertically(i);
            _editor.EnqueueMerge(index, new Frame(_scale.Horizontal, _scale.Vertical));
        }

        public ITableIndex Copy()
            => new MergeScanningIndex(_index.Copy(), _editor);
    }
}