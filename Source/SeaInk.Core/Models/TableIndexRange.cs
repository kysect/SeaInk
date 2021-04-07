using System.IO;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Core.Models
{
    public class TableIndexRange
    {
        public string SheetName { get; set; }
        public int SheetId { get; set; }

        public (int Column, int Row) From { get; set; }
        public (int Column, int Row) To { get; set; }

        public string String => SheetName +
                                $"!{TableIndex.ColumnStringFromInt(From.Column)}{From.Row}" +
                                ":" +
                                $"{TableIndex.ColumnStringFromInt(To.Column)}{To.Row}";

        public TableIndexRange(string sheetName, int sheetId, (int column, int row) from, (int column, int row) to)
        {
            SheetName = sheetName;
            SheetId = sheetId;
            From = from;
            To = to;
        }

        public TableIndexRange(TableIndex from, TableIndex to)
        {
            if (from.SheetName != to.SheetName && from.SheetId != to.SheetId &&
                from.SheetName != "" && to.SheetName != "" &&
                from.SheetId != -1 && to.SheetId != -1)
                throw new InvalidDataException();

            SheetName = from.SheetName == "" ? to.SheetName : from.SheetName;
            SheetId = from.SheetId == -1 ? to.SheetId : from.SheetId;
            From = (from.Column, from.Row);
            To = (to.Column, to.Row);
        }
    }
}