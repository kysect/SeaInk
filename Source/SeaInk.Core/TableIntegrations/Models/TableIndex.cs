using System;

namespace SeaInk.Core.TableIntegrations.Models
{
    public class TableIndex: SheetIndex
    {
        public int Column { get; set; }
        public int Row { get; set; }

        public override string ToString()
        {
            return Name + $"!{ColumnStringFromInt(Column)}{Row + 1}";
        }

        public TableIndex(string sheetName, int sheetId, int column = 0, int row = 0)
            : base(sheetName, sheetId)
        {
            Column = column;
            Row = row;
        }

        public TableIndex(SheetIndex sheetIndex, int column = 0, int row = 0) 
            : this(sheetIndex.Name, sheetIndex.Id, column, row) { }

        public TableIndex WithSheet(SheetIndex index)
            => new TableIndex(index, Column, Row);

        public TableIndex WithColumn(int column)
            => new TableIndex(Name, Id, column, Row);

        public TableIndex WithRow(int row)
            => new TableIndex(Name, Id, Column, row);
        
        
        public TableIndex WithColumnIncreasedBy(int column)
            => new TableIndex(Name, Id, Column + column, Row);

        public TableIndex WithRowIncreasedBy(int row)
            => new TableIndex(Name, Id, Column, Row + row);

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

        public bool Equals(TableIndex rhs)
        {
            return this is SheetIndex a && rhs is SheetIndex b &&
                   a.Equals(b) &&
                   Column == rhs.Column &&
                   Row == rhs.Row;
        }

        public override bool Equals(object obj)
        {
            return obj is TableIndex rhs && Equals(rhs);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Column, Row);
        }
    }
}