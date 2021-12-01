namespace SeaInk.Application.TableLayout.Indices
{
    public class TableIndex : ITableIndex
    {
        public TableIndex(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public int Column { get; private set; }
        public int Row { get; private set; }

        public void MoveHorizontally(int value = 1)
            => Column += value;

        public void MoveVertically(int value = 1)
            => Row += value;

        public ITableIndex Copy()
            => new TableIndex(Column, Row);

        public override string ToString()
            => $"C: {Column}, R: {Row}";
    }
}