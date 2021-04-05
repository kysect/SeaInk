using System.IO;

namespace SeaInk.Core.Models.Tables
{
    public class TableIndex 
    {
        public int Sheet { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public TableIndex(int sheet, int column, int row)
        {
            Sheet = sheet;
            Column = column;
            Row = row;
        }

        public TableIndex(int column, int row)
        {
            Sheet = -1;
            Column = column;
            Row = row;
        }
        
        public TableIndex(int sheet)
        {
            Sheet = sheet;
            Column = 0;
            Row = 0;
        }

        public TableIndex WithSheet(int i)
        {
            return new TableIndex(i, Column, Row);
        }

        public TableIndex WithColumn(int i)
        {
            return new TableIndex(Sheet, i, Row);
        }

        public TableIndex WithRow(int i)
        {
            return new TableIndex(Sheet, Column, i);
        }

        public static TableIndex operator +(TableIndex lhs, TableIndex rhs)
        {
            if (lhs.Sheet != rhs.Sheet && lhs.Sheet != -1 && rhs.Sheet != -1)
                throw new InvalidDataException();

            return new TableIndex(
                lhs.Sheet == -1 ? rhs.Sheet : lhs.Sheet,
                lhs.Column + rhs.Column,
                rhs.Row + rhs.Row);
        }
        
        public static TableIndex operator -(TableIndex lhs, TableIndex rhs)
        {
            if (lhs.Sheet != rhs.Sheet && lhs.Sheet != -1 && rhs.Sheet != -1)
                throw new InvalidDataException();

            return new TableIndex(
                lhs.Sheet == -1 ? rhs.Sheet : lhs.Sheet,
                lhs.Column - rhs.Column,
                rhs.Row - rhs.Row);
        }
    }
}