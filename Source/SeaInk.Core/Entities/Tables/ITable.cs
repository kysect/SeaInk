using System.Collections.Generic;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Core.Entities.Tables
{
    public interface ITable
    {
        int SheetCount { get; }

        int ColumnCount(TableIndex index);
        int RowCount(TableIndex index);

        /// <summary>
        /// Creates a new sheet.
        /// Must throw TableException if creating cannot be performed.
        /// </summary>
        /// <param name="index"> Must contain SheetName and SheetId parameters </param>
        /// <returns></returns>
        void CreateSheet(TableIndex index);

        /// <summary>
        /// Deletes a specified sheet.
        /// Must throw TableException if deleting cannot be performed.
        /// </summary>
        /// <param name="index"> Must contain SheetName and SheetId parameters </param>
        void DeleteSheet(TableIndex index);

        /// <summary>
        /// Loads a sheet at given path.
        /// Must throw TableException if loading cannot be performed.
        /// </summary>
        /// <param name="address"></param>
        void Load(string address);

        /// <summary>
        /// Creates and loads a new table.
        /// Must throw TableException if creating cannot be performed.
        /// </summary>
        /// <returns> Table identifier </returns>
        string Create(string name);

        /// <summary>
        /// Saves loaded sheet.
        /// Must throw TableException if saving cannot be performed.
        /// </summary>
        void Save();

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns> Value casted to specified type </returns>
        T GetValueForCellAt<T>(TableIndex index);

        void Rename(string name);

        void RenameSheet(TableIndex index, string name);
        
        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns> Value casted to string </returns>
        string GetValueForCellAt(TableIndex index);

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="range"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns> Value range casted to specified type </returns>
        List<List<T>> GetValuesForCellsAt<T>(TableIndexRange range);

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// /// <param name="range"></param>
        /// <returns> Value range casted to string </returns>
        List<List<string>> GetValuesForCellsAt(TableIndexRange range);

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        void SetValueForCellAt<T>(TableIndex index, T value);

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="index"> Top left corner of updating range </param>
        /// <param name="values"></param>
        /// <typeparam name="T"></typeparam>
        void SetValuesForCellsAt<T>(TableIndex index, List<List<T>> values);


        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="style"></param>
        void FormatCellAt(TableIndex index, ICellStyle style);

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="range"></param>
        /// <param name="style"></param>
        void FormatCellsAt(TableIndexRange range, ICellStyle style);

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="range"></param>
        void MergeCellsAt(TableIndexRange range);

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="index"></param>
        void DeleteRowAt(TableIndex index);

        /// <summary>
        /// Must throw NonExistingIndexException if index does not exists.
        /// </summary>
        /// <param name="index"></param>
        void DeleteColumnAt(TableIndex index);
    }
}