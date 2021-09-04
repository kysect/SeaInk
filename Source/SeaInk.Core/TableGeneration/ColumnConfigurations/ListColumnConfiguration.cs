using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Models.CellStyle;
using Kysect.CentumFramework.Sheets.Models.CellStyle.Enums;
using Kysect.CentumFramework.Sheets.Models.Indices;

namespace SeaInk.Core.TableGeneration.ColumnConfigurations
{
    public class ListColumnConfiguration : ColumnConfigurationBase
    {
        private readonly string _title;
        private readonly DateTime? _deadline;
        private readonly Func<SheetIndex, SheetIndex, string> _dataFactory;
        private readonly int _size;

        
        public ICellStyle HeaderCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Bold))
        };
        public ICellStyle BodyCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(new LineConfiguration(Color.Black, LineStyle.Light))
        };

        
        public ListColumnConfiguration(string title, IReadOnlyList<string> data, DateTime? deadline = null)
        : this(title, data.Count, (start, i) => data[(int)i.Row - (int)start.Row], deadline)
        {
        }
        
        public ListColumnConfiguration(string title, int size, DateTime? deadline = null)
        : this(title, size, (_, _) => string.Empty, deadline)
        {
        }

        public ListColumnConfiguration(string title, int size, Func<SheetIndex, SheetIndex, string> dataFactory,
            DateTime? deadline = null)
        {
            _title = title;
            _deadline = deadline;
            _dataFactory = dataFactory;
            _size = size;
        }
        
        
        protected override SheetIndex DrawHeader(Sheet sheet, SheetIndex start)
        {
            if (_deadline is not null)
                sheet[start] = _deadline.Value.ToString("M");

            start = start with
            {
                Row = start.Row + 1
            };

            sheet.SetRangeStyle(new SheetIndexRange(start), HeaderCellStyle);
            
            sheet[start] = _title;

            return start with
            {
                Row = start.Row + 1
            };
        }

        protected override SheetIndex DrawBody(Sheet sheet, SheetIndex start)
        {
            var range = new SheetIndexRange(start, start with {Row = start.Row + _size - 1});
            
            sheet.SetRangeStyle(range, BodyCellStyle);

            sheet[range] = range.Select(i => new List<object>
            {
                _dataFactory(start, i)
            }).ToList();

            return start with
            {
                Column = range.To.Column + 1
            };
        }
    }
}