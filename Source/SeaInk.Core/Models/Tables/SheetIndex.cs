using System;
using Google.Apis.Sheets.v4.Data;

namespace SeaInk.Core.Models.Tables
{
    public class SheetIndex
    {
        public string SheetName { get; set; }
        public int SheetId { get; set; }

        public SheetIndex(string sheetName, int sheetId)
        {
            SheetName = sheetName;
            SheetId = sheetId;
        }

        protected bool Equals(SheetIndex other)
            => SheetName == other.SheetName && SheetId == other.SheetId;


        public override bool Equals(object obj)
            => obj is SheetIndex sheet && Equals(sheet);

        public override int GetHashCode()
        {
            return HashCode.Combine(SheetName, SheetId);
        }
    }

    public static class GoogleTableIndexExtension
    {
        public static SheetProperties ToGoogleSheetProperties(this SheetIndex index)
            => new SheetProperties
            {
                Title = index.SheetName,
                SheetId = index.SheetId,
                Index = index.SheetId
            };
    }
}