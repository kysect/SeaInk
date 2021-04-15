using System.Collections;
using System.Collections.Generic;
using SeaInk.Core.Models.Tables.Exceptions;

namespace SeaInk.Core.Models.Tables
{
    public class TableIndexRange : IEnumerable<TableIndex>
    {
        public string SheetName { get; set; }
        public int SheetId { get; set; }

        public (int Column, int Row) From { get; set; }
        public (int Column, int Row) To { get; set; }

        public IEnumerator<TableIndex> GetEnumerator()
        {
            for (int column = From.Column; column < To.Column; ++column)
            {
                for (int row = From.Row; row < To.Row; ++row)
                {
                    yield return new TableIndex(SheetName, SheetId, column, row);
                }
            }
        }

        public override string ToString()
            => SheetName +
               $"!{TableIndex.ColumnStringFromInt(From.Column)}{From.Row + 1}" +
               ":" +
               $"{TableIndex.ColumnStringFromInt(To.Column)}{To.Row + 1}";

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="sheetId"></param>
        /// <param name="from"> Upper left corner </param>
        /// <param name="to"> Lower right corner </param>
        public TableIndexRange(string sheetName, int sheetId, (int column, int row) from, (int column, int row) to)
        {
            SheetName = sheetName;
            SheetId = sheetId;
            From = from;
            To = to;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"> Upper left corner </param>
        /// <param name="to"> Lower right corner </param>
        /// <exception cref="InvalidRangeBoundsException"> Being thrown if given indices located on different sheets </exception>
        public TableIndexRange(TableIndex from, TableIndex to)
        {
            if (from.SheetName != to.SheetName &&
                from.SheetName != "" && to.SheetName != "")
                throw new InvalidRangeBoundsException($"{from.SheetName} - {to.SheetName}");

            if (from.SheetId != to.SheetId &&
                from.SheetId != -1 && to.SheetId != -1)
                throw new InvalidRangeBoundsException($"{from.SheetId} - {to.SheetId}");

            SheetName = from.SheetName == "" ? to.SheetName : from.SheetName;
            SheetId = from.SheetId == -1 ? to.SheetId : from.SheetId;
            From = (from.Column, from.Row);
            To = (to.Column, to.Row);
        }

        public TableIndexRange(TableIndex index)
        {
            SheetName = index.SheetName;
            SheetId = index.SheetId;
            From = (index.Column, index.Row);
            To = (index.Column, index.Row);
        }
    }
}