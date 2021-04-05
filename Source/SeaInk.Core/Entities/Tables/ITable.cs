using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Core.Entities.Tables
{
    public interface ITable
    {
        public int SheetCount { get; }

        public int ColumnCount(TableIndex sheet);
        public int RowCount(TableIndex sheet);

        public void CreateSheet(string name);
        public void DeleteSheet(TableIndex sheet);

        public void Load(string address);

        public string Create();

        public void Save();

        /*
         * 
         * Функция для получения значения ячейки, женерик нужен чтобы
         * сразу кастить в нужный тип
         *
         * Ex (получение баллов студента):
         *
         * try
         * {
         *      int points = Table.GetValueForCellAt<int>((0, 0));
         * }
         * catch (Exception e)
         * {
         *      Console.WriteLine(e.Message);
         * }
         * 
         */
        public T GetValueForCellAt<T>(TableIndex index);
        public string GetValueForCellAt(TableIndex index);

        public List<T> GetValuesForCellsAt<T>(TableIndex index, int count, Direction direction);
        public List<string> GetValuesForCellsAt(TableIndex index, int count, Direction direction);


        public void SetValueForCellAt<T>(TableIndex index, T value);

        public void SetValuesForCellsAt<T>(TableIndex index, List<T> values, Direction direction);


        public void FormatSheet(ISheetMarkup markup, TableIndex sheet);

        //sheets = null - форматировать все листы
        public void FormatSheets(ISheetMarkup markup, TableIndex[]? sheets = null);

        public enum Direction
        {
            Horizontal,
            Vertical
        }
    }
}