using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using SeaInk.Core.TableIntegrations.Google;
using SeaInk.Core.TableIntegrations.Models;
using SeaInk.Core.TableIntegrations.Models.Styles;
using SeaInk.Core.TableIntegrations.Models.Styles.Enums;

namespace SeaInk.Core.TableIntegrations.Excel
{
    public class ExcelTable : ITable
    {
        private XLWorkbook _workbook;
        private string _filePath;
        
        public int SheetCount => _workbook.Worksheets.Count;

        //Method may not be implemented
        public int ColumnCount(TableIndex index) => 0;
        
        //Method may not be implemented
        public int RowCount(TableIndex index) => 0;

        public void CreateSheet(SheetIndex index)
        {
            _workbook.Worksheets.Add(index.Name);
            Save();
        }

        public void DeleteSheet(SheetIndex sheet)
        {
            _workbook.Worksheet(sheet.Name).Delete();
            Save();
        }

        public void Load(TableInfo table)
        {
            _workbook = new XLWorkbook(table.Location);
            _filePath = table.Location;
        }

        public string Create(TableInfo info, DrivePath path)
        {
            //TODO: ensure it's ok
            return Create(info);
        }

        public string Create(TableInfo table)
        {
            _filePath = table.Location;
            
            _workbook = new XLWorkbook();
            _workbook.AddWorksheet("Important Sheet");
            //In workbook must be at least one sheet.
            Save();
            
            return table.Location;
        }

        public void Delete()
        {
            //TODO: ensure it's ok
            throw new System.NotImplementedException();
        }

        public void Rename(string name)
        {
            var oldPath = _filePath;
            var newPath = Path.Combine(Path.GetDirectoryName(_filePath), name);
            _filePath = newPath;
            Save();
            File.Delete(oldPath);
        }

        public void RenameSheet(SheetIndex index, string name)
        {
            _workbook.Worksheet(index.Name).Name = name;
            index.Name = name;
            Save();
        }

        private void Save()
        {
            _workbook.SaveAs(_filePath);
        }

        public T GetValueForCellAt<T>(TableIndex index)
        {
            return (T) _workbook.Worksheet(index.SheetIndex.Name).Cell(index.Row, index.Column).Value;
        }

        public string GetValueForCellAt(TableIndex index)
        {
            return GetValueForCellAt<string>(index);
        }

        public List<List<T>> GetValuesForCellsAt<T>(TableIndexRange range)
        {
            var values = new List<List<T>>();
            foreach (int rowIndex in range.EnumerateRows())
            {
                var newRow = new List<T>();
                foreach (int columnIndex in range.EnumerateColumns())
                {
                    newRow.Add(GetValueForCellAt<T>(new TableIndex(range.SheetIndex, columnIndex+1, rowIndex+1)));
                }

                values.Add(newRow);
            }
            
            return values;
        }

        public List<List<string>> GetValuesForCellsAt(TableIndexRange range)
        {
            return GetValuesForCellsAt<string>(range);
        }

        public void SetValueForCellAt<T>(TableIndex index, T value)
        {
            _workbook.Worksheet(index.SheetIndex.Name).Cell(index.Row, index.Column).Value = value;
            Save();
        }

        public void SetValuesForCellsAt<T>(TableIndex index, List<List<T>> values)
        {
            var width = values.Count;
            var height = values[0].Count;
            
            for (var row = 0; row < width; row++)
            {
                for (var column = 0; column < height; column++)
                {
                    var newIndex = new TableIndex(index.SheetIndex.Name, index.SheetIndex.Id, index.Column + column + 1, index.Row + row + 1);
                    SetValueForCellAt(newIndex, values[row][column]);
                }
            }
            Save();
        }
        
        public void FormatCellAt(TableIndex index, ICellStyle style)
        {
           FormatCellsAt(new TableIndexRange(index), style);
        }

        public void FormatCellsAt(TableIndexRange range, ICellStyle style)
        {
            var cellsRange = _workbook.Worksheet(range.SheetName).Range
            (
                range.From.Row + 1,
                range.From.Column + 1,
                range.To.Row + 1,
                range.To.Column + 1
            );
            
            var worksheet = _workbook.Worksheet(range.SheetIndex.Name);
            for (int columnIndex = range.From.Column+1; columnIndex<=range.To.Column+1;columnIndex++)
                worksheet.Column(columnIndex).Width = style.Width;
            
            foreach (int rowIndex in range.EnumerateRows())
                worksheet.Row(rowIndex).Height = style.Height;

            cellsRange.Style.Fill.SetBackgroundColor(XLColor.FromColor(style.BackgroundColor));
            foreach (var cellIndex in range)
                worksheet.Cell(cellIndex.Row,cellIndex.Column).Hyperlink = new XLHyperlink(style.HyperLink);
            
            cellsRange.Style.Border.SetLeftBorder(style.BorderStyle.Left.Style.ToExcelLineStyle());
            cellsRange.Style.Border.SetLeftBorderColor(XLColor.FromColor(style.BorderStyle.Left.Color));
            
            cellsRange.Style.Border.SetRightBorder(style.BorderStyle.Right.Style.ToExcelLineStyle());
            cellsRange.Style.Border.SetRightBorderColor(XLColor.FromColor(style.BorderStyle.Right.Color));
            
            cellsRange.Style.Border.SetTopBorder(style.BorderStyle.Top.Style.ToExcelLineStyle());
            cellsRange.Style.Border.SetTopBorderColor(XLColor.FromColor(style.BorderStyle.Top.Color));
            
            cellsRange.Style.Border.SetBottomBorder(style.BorderStyle.Bottom.Style.ToExcelLineStyle());
            cellsRange.Style.Border.SetBottomBorderColor(XLColor.FromColor(style.BorderStyle.Bottom.Color));

            cellsRange.Style.Alignment.SetHorizontal(style.HorizontalAlignment.ToExcelHorizontalAlignment());
            cellsRange.Style.Alignment.SetVertical(style.VerticalAlignment.ToExcelVerticalAlignment());

            cellsRange.Style.Font.SetFontName(style.FontName);
            cellsRange.Style.Font.SetFontSize(style.FontSize);
            cellsRange.Style.Font.SetFontColor(XLColor.FromColor(style.FontColor));

            cellsRange.Style.Alignment.WrapText = style.TextWrapping.ToExcelTextWrapping();
            
        }

        public void MergeCellsAt(TableIndexRange range)
        {
            _workbook.Worksheet(range.SheetName).Range
            (
                range.From.Row + 1,
                range.From.Column + 1,
                range.To.Row + 1,
                range.To.Column + 1
            ).Merge();
            
            Save();
        }

        public void DeleteRowAt(TableIndex index)
        {
            _workbook.Worksheet(index.SheetIndex.Name).Row(index.Row + 1).Delete();
            Save();
        }

        public void DeleteColumnAt(TableIndex index)
        {
            _workbook.Worksheet(index.SheetIndex.Name).Column(index.Column + 1).Delete();
            Save();
        }
    }
}