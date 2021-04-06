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

        public TableIndex(int column, int row)
        {
            Sheet = "";
            Column = column;
            Row = row;
        }
        
        public TableIndex(string sheet)
        {
            Sheet = sheet;
            Column = 0;
            Row = 0;
        }

        public TableIndex WithSheet(string i)
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

        public string Range()
        {
            return Sheet + $"!{ConvertToLetters(Column)}{Row}";
        }

        public string Range(TableIndex index)
        {
            if (Sheet != index.Sheet)
                throw new InvalidDataException();

            return Sheet + $"!{ConvertToLetters(Column)}{Row}:{ConvertToLetters(index.Column)}{Row}";
        }

        private string ConvertToLetters(int number)
        {
            string result = "";

            while (number > 0)
            {
                result += 'A' + number % 26;
                number /= 26;
            }

            return result;
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