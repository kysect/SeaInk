using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace SeaInk.Core.Services
{
    public class ExcelGenerator
    {
        private class Index
        {
            private (int, int) _position;
            public Index((int, int) position)
            {
                _position = position;
            }

            public int Column()
            {
                return _position.Item1;
            }
            
            public int Line()
            {
                return _position.Item2;
            }
            
            public string ToExcelIndex()
            {
                return Convert.ToChar(_position.Item1 + 'A' - 1).ToString() + _position.Item2;
            }
        }

        private readonly XLWorkbook _workbook = new XLWorkbook();
        private readonly List<IXLWorksheet> _worksheets = new List<IXLWorksheet>();

        private void AddWorksheet(string nameOfSheet)
        {
            _worksheets.Add(_workbook.Worksheets.Add(nameOfSheet));
        }

        private string GetCellValue(int numberOfSheet, (int, int) position)
        {
            var index = new Index(position).ToExcelIndex();
            return _worksheets[numberOfSheet].Cell(index).Value.ToString();
        }

        private void SetCellBordersThick(int numberOfSheet, (int, int) position)
        {
            var index = new Index(position).ToExcelIndex();
            _worksheets[numberOfSheet].Cell(index).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            _worksheets[numberOfSheet].Cell(index).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            _worksheets[numberOfSheet].Cell(index).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            _worksheets[numberOfSheet].Cell(index).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        }

        private void SetCellsBordersThick(int numberOfSheet, 
            (int, int) leftTopPosition, (int, int) rightBottomPosition)
        {
            var lp = new Index(leftTopPosition);
            var rb = new Index(rightBottomPosition);
            for (var column = lp.Column(); column <= rb.Column(); ++column)
            {
                for (var line = lp.Line(); line <= rb.Line(); ++line)
                {
                    SetCellBordersThick(numberOfSheet, (column, line));
                }
            }
        }

        private void SetCellBordersThin(int numberOfSheet, (int, int) position)
        {
            var index = new Index(position).ToExcelIndex();
            _worksheets[numberOfSheet].Cell(index).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            _worksheets[numberOfSheet].Cell(index).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            _worksheets[numberOfSheet].Cell(index).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            _worksheets[numberOfSheet].Cell(index).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        }

        private void SetCellsBordersThin(int numberOfSheet, 
            (int, int) leftTopPosition, (int, int) rightBottomPosition)
        {
            var lp = new Index(leftTopPosition);
            var rb = new Index(rightBottomPosition);
            for (var column = lp.Column(); column <= rb.Column(); ++column)
            {
                for (var line = lp.Line(); line <= rb.Line(); ++line)
                {
                    SetCellBordersThin(numberOfSheet, (column, line));
                }
            }
        }

        private void SetCellAlignmentCenter(int numberOfSheet, (int, int) position)
        {
            var index = new Index(position).ToExcelIndex();
            _worksheets[numberOfSheet].Cell(index).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            _worksheets[numberOfSheet].Cell(index).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void SetCellsAlignmentCenter(int numberOfSheet, 
            (int, int) leftTopPosition, (int, int) rightBottomPosition)
        {
            var lp = new Index(leftTopPosition);
            var rb = new Index(rightBottomPosition);
            for (var column = lp.Column(); column <= rb.Column(); ++column)
            {
                for (var line = lp.Line(); line <= rb.Line(); ++line)
                {
                    SetCellAlignmentCenter(numberOfSheet, (column, line));
                }
            }
        }

        private void SetCellAutoWidth(int numberOfSheet, (int, int) position)
        {
            var index = new Index(position);
            var data = GetCellValue(numberOfSheet, position);
            
            if (data.Length >= _worksheets[numberOfSheet].Column(index.Column().ToString()).Width)
            {
                _worksheets[numberOfSheet].Column(index.Column().ToString()).Width = data.Length + 1;
            }
        }

        private void SetCellData(int numberOfSheet, (int, int) position, string data)
        {
            var index = new Index(position).ToExcelIndex();
            _worksheets[numberOfSheet].Cell(index).Value = data;
            SetCellAutoWidth(numberOfSheet, position);
        }

        private void SetCellsDataVertical(int numberOfSheet, (int, int) startPosition, List<string> data)
        {
            var index = new Index(startPosition);
            var kline = index.Line();
            foreach (var d in data)
            {
                SetCellData(numberOfSheet, (index.Column(), kline), d);
                SetCellAutoWidth(numberOfSheet, (index.Column(), kline));
                kline++;
            }
        }
        
        private void SetCellsDataHorizontal(int numberOfSheet, (int, int) startPosition, List<string> data)
        {
            var index = new Index(startPosition);
            var kcolumn = index.Column();
            foreach (var d in data)
            {
                SetCellData(numberOfSheet, (kcolumn, index.Line()), d);
                SetCellAutoWidth(numberOfSheet, (kcolumn, index.Line()));
                kcolumn++;
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
            
            SetCellData(0, (1, 1), "ФИО");
            SetCellBordersThick(0, (1, 1));
            SetCellAlignmentCenter(0, (1, 1));

            SetCellsDataVertical(0, (1, 2), names);
            SetCellsDataHorizontal(0, (2, 1), labs);
            
            SetCellsBordersThin(0, (1, 2), (1 + labs.Count, 1 + names.Count));
            
            SetCellsBordersThick(0, (2, 1), (1 + labs.Count, 1));
            
            SetCellsAlignmentCenter(0, (2, 1), (1 + labs.Count, 1));
            
            var path = @"E:\ITMO prog\Projects\Sea-ink\Docs\" + subject + ".xlsx";
            SaveWorkbook(path);
        }
    }
}