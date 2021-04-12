using System.IO;

namespace SeaInk.Core.Models.Tables.Tables
{
    public class TableIndex
    {
        public string SheetName { get; set; }
        public int SheetId { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public string String => SheetName + $"!{ColumnStringFromInt(Column)}{Row + 1}";

        public TableIndex(string sheetName, int sheetId, int column, int row)
        {
            SheetName = sheetName;
            SheetId = sheetId;
            Column = column;
            Row = row;
        }

        public TableIndex(int column, int row)
        {
            SheetName = "";
            SheetId = -1;
            Column = column;
            Row = row;
        }

        public TableIndex(string sheetName, int sheetId)
        {
            SheetName = sheetName;
            SheetId = sheetId;
            Column = 0;
            Row = 0;
        }

        public TableIndex WithSheet(string name, int id)
        {
            return new TableIndex(name, id, Column, Row);
        }

        public TableIndex WithColumn(int column)
        {
            return new TableIndex(SheetName, SheetId, column, Row);
        }

        public TableIndex WithRow(int row)
        {
            return new TableIndex(SheetName, SheetId, Column, row);
        }

        public string Range(TableIndex index)
        {
            if (SheetName != index.SheetName)
                throw new InvalidDataException();

            return SheetName + $"!{ColumnStringFromInt(Column)}{Row + 1}:{ColumnStringFromInt(index.Column)}{Row + 1}";
        }

        public static string ColumnStringFromInt(int number)
        {
            string result = "";

            while (number > 0)
            {
                result += (char) ('A' + number % 26);
                number /= 26;
            }

            return result;
        }

        public static TableIndex operator +(TableIndex lhs, TableIndex rhs)
        {
            if (lhs.SheetName != rhs.SheetName && lhs.SheetId != rhs.SheetId &&
                lhs.SheetName != "" && rhs.SheetName != "" &&
                lhs.SheetId != -1 && rhs.SheetId != -1)
                throw new InvalidDataException();

            return new TableIndex(
                lhs.SheetName == "" ? rhs.SheetName : lhs.SheetName,
                lhs.SheetId == -1 ? rhs.SheetId : lhs.SheetId,
                lhs.Column + rhs.Column,
                rhs.Row + rhs.Row);
        }

        public static TableIndex operator -(TableIndex lhs, TableIndex rhs)
        {
            if (lhs.SheetName != rhs.SheetName && lhs.SheetId != rhs.SheetId &&
                lhs.SheetName != "" && rhs.SheetName != "" &&
                lhs.SheetId != -1 && rhs.SheetId != -1)
                throw new InvalidDataException();

            return new TableIndex(
                lhs.SheetName == "" ? rhs.SheetName : lhs.SheetName,
                lhs.SheetId == -1 ? rhs.SheetId : lhs.SheetId,
                lhs.Column - rhs.Column,
                rhs.Row - rhs.Row);
        }
    }
}