using System.Collections;
using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;
using SeaInk.Core.TableIntegrations.Models.Exceptions;

namespace SeaInk.Core.TableIntegrations.Models
{
    public class TableIndexRange : IEnumerable<TableIndex>
    {
        public SheetIndex SheetIndex { get; private set; }

        public string SheetName
        {
            get => SheetIndex.Name;
            set => SheetIndex.Name = value;
        }

        public int SheetId
        {
            get => SheetIndex.Id;
            set => SheetIndex.Id = value;
        }

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
            => GetEnumerator();

        /// <summary>
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="sheetId"></param>
        /// <param name="from"> Upper left corner </param>
        /// <param name="to"> Lower right corner </param>
        public TableIndexRange(string sheetName, int sheetId, (int column, int row) from, (int column, int row) to)
        {
            SheetIndex = new SheetIndex(sheetName, sheetId);
            From = from;
            To = to;
        }

        /// <summary>
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="from"> Upper left corner </param>
        /// <param name="to"> Lower right corner </param>
        public TableIndexRange(SheetIndex sheetIndex, (int column, int row) from, (int column, int row) to)
        {
            SheetIndex = sheetIndex;
            From = from;
            To = to;
        }

        /// <summary>
        /// </summary>
        /// <param name="from"> Upper left corner </param>
        /// <param name="to"> Lower right corner </param>
        /// <exception cref="InvalidRangeBoundsException"> Being thrown if given indices located on different sheets </exception>
        public TableIndexRange(TableIndex from, TableIndex to)
        {
            if (!Equals(from.SheetIndex, to.SheetIndex))
                throw new InvalidRangeBoundsException($"Trying to create range with incompatible indices\n" +
                                                      $"{System.Text.Json.JsonSerializer.Serialize(from)}\n\n" +
                                                      $"{System.Text.Json.JsonSerializer.Serialize(to)}");

            SheetIndex = from.SheetIndex;
            From = (from.Column, from.Row);
            To = (to.Column, to.Row);
        }

        public TableIndexRange(TableIndex index)
            : this(index, index) { }
    }

    public static class GoogleTableIndexRangeExtension
    {
        public static GridRange ToGoogleGridRange(this TableIndexRange range)
            => new GridRange
            {
                SheetId = range.SheetId,
                StartColumnIndex = range.From.Column,
                StartRowIndex = range.From.Row,
                EndColumnIndex = range.To.Column + 1,
                EndRowIndex = range.To.Row + 1
            };
    }
}