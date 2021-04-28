using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using SeaInk.Core.Models.Google;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Core.Entities.Tables
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
            var counter = 0;
            var width = range.From.Column - range.To.Column;

            var line = new List<T>();
            foreach (var index in range)
            {
                line.Add(GetValueForCellAt<T>(index));
                counter++;

                if (counter == width)
                {
                    counter = 0;
                    values.Add(line);
                    line.Clear();
                }
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
            //Method may not be implemented
            //Throwing an exception crashes main program
        }

        public void FormatCellsAt(TableIndexRange range, ICellStyle style)
        {
            //Method may not be implemented
            //Throwing an exception crashes main program
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