using System.Collections.Generic;
using SeaInk.Core.Models.Tables;
using SeaInk.Core.Models.Tables.Enums;

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

        /// <summary>
        /// Returns value casted to specified type
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetValueForCellAt<T>(TableIndex index);
        string GetValueForCellAt(TableIndex index);

        /// <summary>
        /// Returns value range casted to specified type
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="direction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetValuesForCellsAt<T>(TableIndex index, int count, Direction direction);
        List<string> GetValuesForCellsAt(TableIndex index, int count, Direction direction);


        void SetValueForCellAt<T>(TableIndex index, T value);

        void SetValuesForCellsAt<T>(TableIndex index, List<T> values, Direction direction);


        void FormatSheet(ISheetMarkup markup, TableIndex sheet);

        //sheets = null - форматировать все листы
        void FormatSheets(ISheetMarkup markup, TableIndex[]? sheets = null);
    }
}