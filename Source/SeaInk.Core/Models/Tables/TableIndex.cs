using Google.Apis.Sheets.v4.Data;

namespace SeaInk.Core.Models.Tables
{
    public class TableIndex
    {
        public string SheetName { get; set; }
        public int SheetId { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public override string ToString()
        {
            return SheetName + $"!{ColumnStringFromInt(Column)}{Row + 1}";
        }

        public TableIndex(string sheetName, int sheetId, int column = 0, int row = 0)
        {
            SheetName = sheetName;
            SheetId = sheetId;
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

    public static class GoogleTableIndexExtension
    {
        public static SheetProperties ToGoogleSheetProperties(this TableIndex index)
            => new SheetProperties
            {
                Title = index.SheetName,
                SheetId = index.SheetId,
                Index = index.SheetId
            };
    }
}