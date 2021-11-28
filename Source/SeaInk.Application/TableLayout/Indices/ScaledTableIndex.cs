using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Indices
{
    public class ScaledTableIndex : IScaledTableIndex
    {
        private readonly ITableIndex _index;

        public ScaledTableIndex(Scale scale, ITableIndex index)
        {
            Scale = scale;
            _index = index;
        }

        public Scale Scale { get; }

        public int Column => _index.Column;
        public int Row => _index.Row;

        public void MoveHorizontally(int i = 1)
            => _index.MoveHorizontally(i * Scale.Horizontal);

        public void MoveVertically(int i = 1)
            => _index.MoveVertically(i * Scale.Vertical);

        public ITableIndex Copy()
            => new ScaledTableIndex(Scale, _index.Copy());

        public override string ToString()
            => _index.ToString() ?? string.Empty;
    }
}