using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace SeaInk.Core.Services
{
    public class ExcelGenerator

    {
        private readonly XLWorkbook _workbook = new XLWorkbook();
        private readonly List<IXLWorksheet> _worksheets = new List<IXLWorksheet>();

        private (char, int) ParsePosition(string position)
        {
            char column = ' ';
            string line = "";

            foreach (var i in position)
            {
                if (i >= '0' && i <= '9')
                {
                    line += i;
                }
                else
                {
                    column = i;
                }
            }

            return (column, int.Parse(line));
        }

        private void AddWorksheet(string nameOfSheet)
        {
            _worksheets.Add(_workbook.Worksheets.Add(nameOfSheet));
        }

        private void SetCellBordersThick(int numberOfSheet, string position)
        {
            _worksheets[numberOfSheet].Cell(position).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            _worksheets[numberOfSheet].Cell(position).Style.Border.TopBorder = XLBorderStyleValues.Thick;
            _worksheets[numberOfSheet].Cell(position).Style.Border.RightBorder = XLBorderStyleValues.Thick;
            _worksheets[numberOfSheet].Cell(position).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        }

        private void SetCellsBordersThick(int numberOfSheet, string leftTopPosition, string rightBottomPosition)
        {
            char columnLT, columnRB;
            int lineLT, lineRB;
            (columnLT, lineLT) = ParsePosition(leftTopPosition);
            (columnRB, lineRB) = ParsePosition(rightBottomPosition);

            for (char i = columnLT; i < columnRB || i == columnLT; ++i)
            {
                for (int j = lineLT; j < lineRB || j == lineLT; ++j)
                {
                    SetCellBordersThick(numberOfSheet, i.ToString() + j);
                }
            }
        }

        private void SetCellBordersThin(int numberOfSheet, string position)
        {
            _worksheets[numberOfSheet].Cell(position).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            _worksheets[numberOfSheet].Cell(position).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            _worksheets[numberOfSheet].Cell(position).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            _worksheets[numberOfSheet].Cell(position).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        }

        private void SetCellsBordersThin(int numberOfSheet, string leftTopPosition, string rightBottomPosition)
        {
            char columnLT, columnRB;
            int lineLT, lineRB;
            (columnLT, lineLT) = ParsePosition(leftTopPosition);
            (columnRB, lineRB) = ParsePosition(rightBottomPosition);

            for (char i = columnLT; i < columnRB || i == columnLT; ++i)
            {
                for (int j = lineLT; j < lineRB || j == lineLT; ++j)
                {
                    SetCellBordersThin(numberOfSheet, i.ToString() + j);
                }
            }
        }

        private void SetCellAlignmentCenter(int numberOfSheet, string position)
        {
            _worksheets[numberOfSheet].Cell(position).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            _worksheets[numberOfSheet].Cell(position).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void SetCellsAlignmentCenter(int numberOfSheet, string leftTopPosition, string rightBottomPosition)
        {
            char columnLT, columnRB;
            int lineLT, lineRB;
            (columnLT, lineLT) = ParsePosition(leftTopPosition);
            (columnRB, lineRB) = ParsePosition(rightBottomPosition);

            for (char i = columnLT; i < columnRB || i == columnLT; ++i)
            {
                for (int j = lineLT; j < lineRB || j == lineLT; ++j)
                {
                    SetCellAlignmentCenter(numberOfSheet, i.ToString() + j);
                }
            }
        }

        private void SetCellData(int numberOfSheet, string position, string data)
        {
            _worksheets[numberOfSheet].Cell(position).Value = data;
        }

        private void SetCellsDataVertical(int numberOfSheet, string startCell, List<string> data)
        {
            char column;
            int line;
            (column, line) = ParsePosition(startCell);

            var k = line;
            foreach (var i in data)
            {
                _worksheets[numberOfSheet].Cell(column.ToString() + k).Value = i;
                
                if (i.Length >= _worksheets[numberOfSheet].Column(column.ToString()).Width)
                {
                    _worksheets[numberOfSheet].Column(column.ToString()).Width = i.Length + 1;
                }
                k++;
            }
        }
        
        private void SetCellsDataHorizontal(int numberOfSheet, string startCell, List<string> data)
        {
            char column;
            int line;
            (column, line) = ParsePosition(startCell);

            var k = column;
            foreach (var i in data)
            {
                _worksheets[numberOfSheet].Cell(k.ToString() + line).Value = i;

                if (i.Length >= _worksheets[numberOfSheet].Column(k.ToString()).Width)
                {
                    _worksheets[numberOfSheet].Column(k.ToString()).Width = i.Length + 1;
                }
                k++;
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

            SetCellData(0, "A" + 1, "ФИО");
            SetCellBordersThick(0, "A" + 1);
            SetCellAlignmentCenter(0, "A" + 1);

            SetCellsDataVertical(0, "A" + 2, names);
            SetCellsDataHorizontal(0, "B" + 1, labs);

            var lastCell = Convert.ToChar('B' + labs.Count).ToString() + (2 + names.Count);
            SetCellsBordersThin(0, "A" + 2, lastCell);
            
            lastCell = Convert.ToChar('B' + labs.Count).ToString() + 1;
            SetCellsBordersThick(0, "B" + 1, lastCell);
            SetCellsAlignmentCenter(0, "B" + 1, lastCell);

            var path = @"E:\ITMO prog\Projects\Sea-ink\Docs\" + subject + ".xlsx";
            SaveWorkbook(path);
        }
    }
}