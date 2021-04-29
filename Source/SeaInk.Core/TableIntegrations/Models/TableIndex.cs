namespace SeaInk.Core.TableIntegrations.Models
{
    public class TableIndex
    {
        public SheetIndex SheetIndex { get; private set; }

        public int Column { get; set; }
        public int Row { get; set; }

        public override string ToString()
        {
            return SheetIndex.Name + $"!{ColumnStringFromInt(Column)}{Row + 1}";
        }

        public TableIndex(string sheetName, int sheetId, int column = 0, int row = 0)
            : this(new SheetIndex(sheetName, sheetId), column, row) { }

        public TableIndex(SheetIndex sheetIndex, int column = 0, int row = 0)
        {
            SheetIndex = sheetIndex;
            Column = column;
            Row = row;
        }

        public TableIndex WithSheet(SheetIndex index)
            => new TableIndex(index, Column, Row);

        public TableIndex WithColumn(int column)
            => new TableIndex(SheetIndex.Name, SheetIndex.Id, column, Row);

        public TableIndex WithRow(int row)
            => new TableIndex(SheetIndex.Name, SheetIndex.Id, Column, row);
        
        
        public TableIndex WithColumnIncreasedBy(int column)
            => new TableIndex(SheetIndex.Name, SheetIndex.Id, Column + column, Row);

        public TableIndex WithRowIncreasedBy(int row)
            => new TableIndex(SheetIndex.Name, SheetIndex.Id, Column, Row + row);

        public static string ColumnStringFromInt(int number)
        {
            string result = "";

            do
            {
                result = (char) ('A' + number % 26) + result;
                number /= 26;
            } while (number >= 26);

            if (number != 0)
                result = (char) ('A' + number - 1) + result;

            return result;
        }
    }
}