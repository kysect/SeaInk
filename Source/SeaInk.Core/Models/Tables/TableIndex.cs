using Google.Apis.Sheets.v4.Data;

namespace SeaInk.Core.Models.Tables
{
    public class TableIndex
    {
        public SheetIndex SheetIndex { get; private set; }

        public string SheetName
        {
            get => SheetIndex.SheetName;
            set => SheetIndex.SheetName = value;
        }

        public int SheetId
        {
            get => SheetIndex.SheetId;
            set => SheetIndex.SheetId = value;
        }

        public int Column { get; set; }
        public int Row { get; set; }

        public override string ToString()
        {
            return SheetName + $"!{ColumnStringFromInt(Column)}{Row + 1}";
        }

        public TableIndex(string sheetName, int sheetId, int column = 0, int row = 0)
            : this(new SheetIndex(sheetName, sheetId), column, row) { }

        public TableIndex(SheetIndex sheetIndex, int column = 0, int row = 0)
        {
            SheetIndex = sheetIndex;
            Column = column;
            Row = row;
        }

        public TableIndex WithSheet(string name, int id)
            => new TableIndex(name, id, Column, Row);

        public TableIndex WithColumn(int column)
            => new TableIndex(SheetName, SheetId, column, Row);

        public TableIndex WithRow(int row)
            => new TableIndex(SheetName, SheetId, Column, row);


        public TableIndex WithColumnIncreasedBy(int column)
            => new TableIndex(SheetName, SheetId, Column + column, Row);

        public TableIndex WithRowIncreasedBy(int row)
            => new TableIndex(SheetName, SheetId, Column, Row + row);

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