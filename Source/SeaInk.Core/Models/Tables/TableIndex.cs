using System.IO;

namespace SeaInk.Core.Models.Tables
{
    public class TableIndex 
    {
        public string Sheet { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public TableIndex(string sheet, int column, int row)
        {
            Sheet = sheet;
            Column = column;
            Row = row;
        }
        
        public TableIndex(string sheet)
        {
            Sheet = sheet;
            Column = 0;
            Row = 0;
        }

        public static TableIndex operator +(TableIndex lhs, TableIndex rhs)
        {
            if (lhs.Sheet != rhs.Sheet && lhs.Sheet != "" && rhs.Sheet != "")
                throw new InvalidDataException();

            return new TableIndex(
                lhs.Sheet == "" ? rhs.Sheet : lhs.Sheet,
                lhs.Column + rhs.Column,
                rhs.Row + rhs.Row);
        }
        
        public static TableIndex operator -(TableIndex lhs, TableIndex rhs)
        {
            if (lhs.Sheet != rhs.Sheet && lhs.Sheet != "" && rhs.Sheet != "")
                throw new InvalidDataException();

            return new TableIndex(
                lhs.Sheet == "" ? rhs.Sheet : lhs.Sheet,
                lhs.Column - rhs.Column,
                rhs.Row - rhs.Row);
        }
    }
}