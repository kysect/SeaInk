using System.Collections;
using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;
using SeaInk.Core.TableIntegrations.Models.Exceptions;

namespace SeaInk.Core.TableIntegrations.Models
{
    public class TableIndexRange : SheetIndex, IEnumerable<TableIndex>
    {
        public (int Column, int Row) From { get; set; }
        public (int Column, int Row) To { get; set; }

        public IEnumerator<TableIndex> GetEnumerator()
        {
            for (int column = From.Column; column < To.Column; ++column)
            {
                for (int row = From.Row; row < To.Row; ++row)
                {
                    yield return new TableIndex(Name, Id, column, row);
                }
            }
        }

        public IEnumerable EnumerateRows()
        {
            for (int row = From.Row; row < To.Row; row++)
            {
                yield return new TableIndexRange(Name, Id, (From.Column, row), (To.Column, row));
            }
        }

        public IEnumerable EnumerateColumns()
        {
            for (int column = From.Column; column < To.Column; column++)
            {
                yield return new TableIndexRange(Name, Id, (column, From.Row), (column, To.Row));
            }
        }

        public IEnumerable EnumerateRowsIndices()
        {
            for (int row = From.Row; row < To.Row; row++)
            {
                yield return row;
            }
        }

        public IEnumerable EnumerateColumnsIndices()
        {
            for (int column = From.Column; column < To.Column; column++)
            {
                yield return column;
            }
        }

        public override string ToString()
            => Name +
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
            : base(sheetName, sheetId)
        {
            From = from;
            To = to;
        }

        /// <summary>
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="from"> Upper left corner </param>
        /// <param name="to"> Lower right corner </param>
        public TableIndexRange(SheetIndex sheetIndex, (int column, int row) from, (int column, int row) to)
            : this(sheetIndex.Name, sheetIndex.Id, from, to) { }

        /// <summary>
        /// </summary>
        /// <param name="from"> Upper left corner </param>
        /// <param name="to"> Lower right corner </param>
        /// <exception cref="InvalidRangeBoundsException"> Being thrown if given indices located on different sheets </exception>
        public TableIndexRange(TableIndex from, TableIndex to)
        {
            if (from is SheetIndex lhs && to is SheetIndex rhs && !Equals(lhs, rhs))
                throw new InvalidRangeBoundsException($"Trying to create range with incompatible indices\n" +
                                                      $"{System.Text.Json.JsonSerializer.Serialize(from)}\n\n" +
                                                      $"{System.Text.Json.JsonSerializer.Serialize(to)}");

            Name = from.Name;
            Id = from.Id;
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
                SheetId = range.Id,
                StartColumnIndex = range.From.Column,
                StartRowIndex = range.From.Row,
                EndColumnIndex = range.To.Column + 1,
                EndRowIndex = range.To.Row + 1
            };
    }
}