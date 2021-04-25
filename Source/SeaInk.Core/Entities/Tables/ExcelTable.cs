using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Core.Entities.Tables
{
    public class ExcelTable : ITable
    {
        private XLWorkbook _workbook = null!;
        private string _pathToFile;


        public int SheetCount => _workbook.Worksheets.Count;
        
        public int ColumnCount(TableIndex index)
        {
            return 0;
        }

        public int RowCount(TableIndex index)
        {
            return 0;
        }

        public void CreateSheet(TableIndex index)
        {
            _workbook.Worksheets.Add(index.SheetName);
            Save();
        }

        public void DeleteSheet(TableIndex sheet)
        {
            _workbook.Worksheets.Delete(sheet.SheetName);

            var oldPath = _pathToFile;
            _pathToFile = SeparatePath(_pathToFile).path;
            _pathToFile += "t.xlsx";
            
            Save();
            File.Delete(oldPath);
            
            Rename(SeparatePath(oldPath).name);

        }

        public void Load(string address)
        {
            _workbook = new XLWorkbook(address);
            _pathToFile = address;
            Save();
        }

        public string Create(string path)
        {
            _workbook = new XLWorkbook();
            _workbook.AddWorksheet("Important Sheet");
            
            _pathToFile = path;
            return path;
        }

        private (string path, string name) SeparatePath(string pathToFile)
        {

            var array = new List<string>();
            var word = "";
            for (var i = 0; i < pathToFile.Length; i++)
            {
                word += pathToFile[i];
                if (pathToFile[i] == '\\' || i == pathToFile.Length - 1)
                {
                    array.Add(word);
                    word = "";
                }
            }
            
            var path = "";
            for (var i = 0; i < array.Count - 1; ++i)
            {
                path += array[i];
            }

            (string path, string name) t;
            t.path = path;
            t.name = array[^1];
            return t;
        }
        
        public void Rename(string name)
        {
            var oldPath = _pathToFile;
            _pathToFile = SeparatePath(_pathToFile).path;
            _pathToFile += name;
            
            Save();
            File.Delete(oldPath);
        }

        public void RenameSheet(TableIndex index, string name)
        {
            _workbook.Worksheet(index.SheetName).Name = name;
            Save();
        }

        private void Save()
        {
            if (File.Exists(_pathToFile))
            {
                File.Delete(_pathToFile);
            }

            _workbook.SaveAs(_pathToFile);
        }

        public T GetValueForCellAt<T>(TableIndex index)
        {
            return (T) _workbook.Worksheet(index.SheetName).Cell(index.Row, index.Column).Value;
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
            _workbook.Worksheet(index.SheetName).Cell(index.Row, index.Column).Value = value!.ToString();
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
                    var newIndex = new TableIndex(index.SheetName, index.SheetId, index.Column + column + 1, index.Row +row + 1);
                    SetValueForCellAt(newIndex, values[row][column]);
                }
            }
            Save();
        }

        public void FormatCellAt(TableIndex index, ICellStyle style)
        {
        }

        public void FormatCellsAt(TableIndexRange range, ICellStyle style)
        {
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
            _workbook.Worksheet(index.SheetName).Row(index.Row + 1).Delete();
            Save();
        }

        public void DeleteColumnAt(TableIndex index)
        {
            _workbook.Worksheet(index.SheetName).Column(index.Column + 1).Delete();
            Save();
        }
    }
}