using System.Collections.Generic;
using SeaInk.Core.Models;
using SeaInk.Core.Models.Tables;
using SeaInk.Core.Models.Tables.Enums;

namespace SeaInk.Core.Entities.Tables
{
    public interface ITable
    {
        int SheetCount { get; }

        int ColumnCount(TableIndex index);
        int RowCount(TableIndex index);

        void CreateSheet(TableIndex index);
        void DeleteSheet(TableIndex index);

        void Load(string address);

        string Create(string name);

        void Delete();

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
        /// <param name="range"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<List<T>> GetValuesForCellsAt<T>(TableIndexRange range);
        List<List<string>> GetValuesForCellsAt(TableIndexRange range);


        void SetValueForCellAt<T>(TableIndex index, T value);

        void SetValuesForCellsAt<T>(TableIndex index, List<List<T>> values);


        void FormatSheet(ISheetMarkup markup, TableIndex sheet);

        /// <summary>
        /// sheets == null - Format all sheets
        /// </summary>
        /// <param name="markup"></param>
        /// <param name="sheets"></param>
        void FormatSheets(ISheetMarkup markup, TableIndex[]? sheets = null);
    }
}