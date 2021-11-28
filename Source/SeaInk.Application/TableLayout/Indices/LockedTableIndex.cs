using SeaInk.Application.Exceptions;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Indices
{
    public class LockedTableIndex : ITableIndex
    {
        private readonly ITableIndex _index;
        private readonly ITableIndex _begin;
        private readonly Frame _frame;

        public LockedTableIndex(ITableIndex index, Frame frame)
        {
            _index = index.ThrowIfNull(nameof(index));
            _begin = index.Copy();
            _frame = frame.ThrowIfNull(nameof(frame));
        }

        private LockedTableIndex(ITableIndex index, ITableIndex begin, Frame frame)
        {
            _index = index;
            _begin = begin;
            _frame = frame;
        }

        public int Column => _index.Column;
        public int Row => _index.Row;

        public void MoveHorizontally(int i = 1)
        {
            _index.MoveHorizontally(i);

            int diff = _index.Column - _begin.Column;

            if (diff < 0 || diff >= _frame.Width)
                throw new CrossedFrameException(_begin, _index, _frame);
        }

        public void MoveVertically(int i = 1)
        {
            _index.MoveVertically(i);

            int diff = _index.Row - _begin.Row;

            if (diff < 0 || diff >= _frame.Height)
                throw new CrossedFrameException(_begin, _index, _frame);
        }

        public ITableIndex Copy()
            => new LockedTableIndex(_index.Copy(), _begin, _frame);

        public override string ToString()
            => _index.ToString() ?? string.Empty;
    }
}