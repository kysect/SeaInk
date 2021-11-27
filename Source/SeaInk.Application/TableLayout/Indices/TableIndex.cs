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

        public void MoveHorizontally(int i = 1)
            => Column += i;

        public void MoveVertically(int i = 1)
            => Row += i;

        public ITableIndex Copy()
            => new TableIndex(Column, Row);
    }
}