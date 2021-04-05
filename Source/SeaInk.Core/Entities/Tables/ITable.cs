using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Core.Entities.Tables
{
    public interface ITable
    {
        int SheetCount { get; }

        int ColumnCount(TableIndex sheet);
        int RowCount(TableIndex sheet);

        void CreateSheet(string name);
        void DeleteSheet(TableIndex sheet);

        void Load(string address);

        string Create();

        void Save();

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
        T GetValueForCellAt<T>(TableIndex index);
        string GetValueForCellAt(TableIndex index);

        List<T> GetValuesForCellsAt<T>(TableIndex index, int count, Direction direction);
        List<string> GetValuesForCellsAt(TableIndex index, int count, Direction direction);


        void SetValueForCellAt<T>(TableIndex index, T value);

        void SetValuesForCellsAt<T>(TableIndex index, List<T> values, Direction direction);


        void FormatSheet(ISheetMarkup markup, TableIndex sheet);

        //sheets = null - форматировать все листы
        void FormatSheets(ISheetMarkup markup, TableIndex[]? sheets = null);

        enum Direction
        {
            Horizontal,
            Vertical
        }
    }
}