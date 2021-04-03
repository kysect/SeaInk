using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace SeaInk.Core.Services
{
    public class ExcelGenerator
    {
        private class Index
        {
            public int Column { get; }
            public int Line { get; }
            
            public Index(int column, int line)
            {
                Column = column;
                Line = line;
            }
            
            public string ToExcelIndex()
            {
                return Convert.ToChar(Column + 'A' - 1).ToString() + Line;
            }
        }

        private readonly XLWorkbook _workbook = new XLWorkbook();
        private readonly List<IXLWorksheet> _worksheets = new List<IXLWorksheet>();

        private void AddWorksheet(string nameOfSheet)
        {
            _worksheets.Add(_workbook.Worksheets.Add(nameOfSheet));
        }

        private string GetCellValue(int sheet, int column, int line)
        {
            var index = new Index(column, line).ToExcelIndex();
            return _worksheets[sheet].Cell(index).Value.ToString();
        }
        
        private IEnumerable GetCells(Index lp, Index rb)
        {
            for (var column = lp.Column; column <= rb.Column; column++)
            {
                for (var line = lp.Line; line <= rb.Line; line++)
                {
                    yield return new Index(column, line);
                }
            }
        }

        private void SetCellBordersThick(int sheet, int column, int line)
        {
            var index = new Index(column, line).ToExcelIndex();
            _worksheets[sheet].Cell(index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            _worksheets[sheet].Cell(index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            _worksheets[sheet].Cell(index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            _worksheets[sheet].Cell(index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        }

        private void SetCellsBordersThick(int sheet, int leftTopColumn, int leftTopLine, int rightBotColumn, int rightBotLine)
        {
            var lp = new Index(leftTopColumn, leftTopLine);
            var rb = new Index(rightBotColumn, rightBotLine);

            foreach (Index index in GetCells(lp, rb))
            {
                SetCellBordersThick(sheet, index.Column, index.Line);
            }
        }

        private void SetCellBordersThin(int sheet, int column, int line)
        {
            var index = new Index(column, line).ToExcelIndex();
            _worksheets[sheet].Cell(index).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            _worksheets[sheet].Cell(index).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            _worksheets[sheet].Cell(index).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            _worksheets[sheet].Cell(index).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        }

        private void SetCellsBordersThin(int sheet, int leftTopColumn, int leftTopLine, int rightBotColumn, int rightBotLine)
        {
            var lp = new Index(leftTopColumn, leftTopLine);
            var rb = new Index(rightBotColumn, rightBotLine);

            foreach (Index index in GetCells(lp, rb))
            {
                SetCellBordersThin(sheet, index.Column, index.Line);
            }
        }

        private void SetCellAlignmentCenter(int sheet, int column, int line)
        {
            var index = new Index(column, line).ToExcelIndex();
            _worksheets[sheet].Cell(index).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            _worksheets[sheet].Cell(index).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void SetCellsAlignmentCenter(int sheet, int leftTopColumn, int leftTopLine, int rightBotColumn, int rightBotLine)
        {
            var lp = new Index(leftTopColumn, leftTopLine);
            var rb = new Index(rightBotColumn, rightBotLine);

            foreach (Index index in GetCells(lp, rb))
            {
                SetCellAlignmentCenter(sheet, index.Column, index.Line);
            }
        }

        private void SetCellAutoWidth(int sheet, int column, int line)
        {
            var index = new Index(column, line);
            var data = GetCellValue(sheet, column, line);
            
            if (data.Length >= _worksheets[sheet].Column(index.Column.ToString()).Width)
            {
                _worksheets[sheet].Column(index.Column.ToString()).Width = data.Length + 1;
            }
        }

        private void SetCellData(int sheet, int column, int line, string data)
        {
            var index = new Index(column, line).ToExcelIndex();
            _worksheets[sheet].Cell(index).Value = data;
            SetCellAutoWidth(sheet, column, line);
        }

        private void SetCellsDataVertical(int sheet, int column, int line, List<string> data)
        {
            var index = new Index(column, line);
            var numberOfLine = index.Line;
            foreach (var d in data)
            {
                SetCellData(sheet, index.Column, numberOfLine, d);
                SetCellAutoWidth(sheet, index.Column, numberOfLine);
                numberOfLine++;
            }
        }
        
        private void SetCellsDataHorizontal(int sheet, int column, int line, List<string> data)
        {
            var index = new Index(column, line);
            var numberOfColumn = index.Column;
            foreach (var d in data)
            {
                SetCellData(sheet, numberOfColumn, index.Line, d);
                SetCellAutoWidth(sheet, numberOfColumn, index.Line);
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

            SetCellsDataVertical(0, 1, 2, names);
            SetCellsDataHorizontal(0, 2, 1, labs);
            
            SetCellsBordersThin(0, 1, 2, 1 + labs.Count, 1 + names.Count);
            
            SetCellsBordersThick(0, 2, 1, 1 + labs.Count, 1);
            
            SetCellsAlignmentCenter(0, 2, 1, 1 + labs.Count, 1);
            
            var path = @"E:\ITMO prog\Projects\Sea-ink\Docs\" + subject + ".xlsx";
            SaveWorkbook(path); 
        }
    }
}