using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Column = DocumentFormat.OpenXml.Wordprocessing.Column;

namespace SeaInk.Core.Services
{
    public class ExcelGenerator
    {
        private readonly XLWorkbook _workbook = new XLWorkbook();
        private readonly List<IXLWorksheet> _worksheets = new List<IXLWorksheet>();

        private void AddWorksheet(string nameOfSheet)
        {
            _worksheets.Add(_workbook.Worksheets.Add(nameOfSheet));
        }

        private string GetCellValue(int sheet, int row, int column)
        {
            return _worksheets[sheet].Cell(row, column).Value.ToString();
        }
        
        private IEnumerable GetCells(int startRow, int startColumn, int endRow, int endColumn)
        {
            for (var column = startColumn; column <= endColumn; column++)
            {
                for (var row = startRow; row <= endRow; row++)
                {
                    (int row, int column) index;
                    index.row = row;
                    index.column = column;
                    yield return index;
                }
            }
        }

        private void SetCellBordersThick(int sheet, int row, int column)
        {
            _worksheets[sheet].Cell(row, column).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            _worksheets[sheet].Cell(row, column).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            _worksheets[sheet].Cell(row, column).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            _worksheets[sheet].Cell(row, column).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        }

        private void SetCellsBordersThick(int sheet, int startRow, int startColumn, int endRow, int endColumn)
        {
            foreach ((int row, int column) index in GetCells(startRow, startColumn, endRow, endColumn))
            {
                SetCellBordersThick(sheet, index.row, index.column);
            }
        }

        private void SetCellBordersThin(int sheet, int row, int column)
        {
            _worksheets[sheet].Cell(row, column).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            _worksheets[sheet].Cell(row, column).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            _worksheets[sheet].Cell(row, column).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            _worksheets[sheet].Cell(row, column).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        }

        private void SetCellsBordersThin(int sheet, int startRow, int startColumn, int endRow, int endColumn)
        {
            foreach ((int row, int column) index in GetCells(startRow, startColumn, endRow, endColumn))
            {
                SetCellBordersThin(sheet, index.row, index.column);
            }
        }

        private void SetCellAlignmentCenter(int sheet, int row, int column)
        {
            _worksheets[sheet].Cell(row, column).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            _worksheets[sheet].Cell(row, column).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void SetCellsAlignmentCenter(int sheet, int startRow, int startColumn, int endRow, int endColumn)
        {
            foreach ((int column, int row) index in GetCells(startRow, startColumn, endRow, endColumn))
            {
                SetCellAlignmentCenter(sheet, index.row, index.column);
            }
        }

        private void SetCellAutoWidth(int sheet, int row, int column)
        {
            var data = GetCellValue(sheet, row, column);
            
            if (data.Length >= _worksheets[sheet].Column(column).Width)
            {
                _worksheets[sheet].Column(column).Width = data.Length + 1;
            }
        }

        private void SetCellData(int sheet, int row, int column, string data)
        {
            _worksheets[sheet].Cell(row, column).Value = data;
            SetCellAutoWidth(sheet, row, column);
        }

        private void SetCellsDataVertical(int sheet, int row, int column, List<string> data)
        {
            var numberOfRow = row;
            foreach (var d in data)
            {
                SetCellData(sheet, numberOfRow, column, d);
                SetCellAutoWidth(sheet, numberOfRow, column);
                numberOfRow++;
            }
        }
        
        private void SetCellsDataHorizontal(int sheet, int row, int column, List<string> data)
        {
            var numberOfColumn = column;
            foreach (var d in data)
            {
                SetCellData(sheet, row, numberOfColumn, d);
                SetCellAutoWidth(sheet, row, numberOfColumn);
                numberOfColumn++;
            }
        }

        private void SaveWorkbook(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            _workbook.SaveAs(path);
        }

        public void GenerateWorkbook(string subject, List<string> names, List<string> labs)
        {
            //TODO: Ширина таблицы ограничена английским алфавитом - когда-то исправить
            AddWorksheet(subject);

            SetCellData(0, 1, 1, "ФИО");
            SetCellBordersThick(0, 1, 1);
            SetCellAlignmentCenter(0, 1, 1);

            SetCellsDataVertical(0, 2, 1, names);
            SetCellsDataHorizontal(0, 1, 2, labs);
            
            SetCellsBordersThin(0, 2, 1, 1 + names.Count, 1 + labs.Count);
            
            SetCellsBordersThick(0, 1, 2, 1, 1 + labs.Count);
            
            SetCellsAlignmentCenter(0, 1, 2, 1, 1 + labs.Count);
            
            var path = @"E:\ITMO prog\Projects\Sea-ink\Docs\" + subject + ".xlsx";
            SaveWorkbook(path); 
        }
    }
}