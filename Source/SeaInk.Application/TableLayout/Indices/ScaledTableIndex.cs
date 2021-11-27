using SeaInk.Application.TableLayout.Models;

namespace SeaInk.Application.TableLayout.Indices
{
    public class ScaledTableIndex : ITableIndex
    {
        private readonly Scale _scale;
        private readonly ITableIndex _index;

        public ScaledTableIndex(Scale scale, ITableIndex index)
        {
            _scale = scale;
            _index = index;
        }

        public int Column => _index.Column;
        public int Row => _index.Row;

        public void MoveHorizontally(int i = 1)
            => _index.MoveHorizontally(i * _scale.Horizontal);

        public void MoveVertically(int i = 1)
            => _index.MoveVertically(i * _scale.Vertical);

        public ITableIndex Copy()
            => new ScaledTableIndex(_scale, _index.Copy());

        public override string ToString()
            => $"C: {Column}, R: {Row}";
    }
}