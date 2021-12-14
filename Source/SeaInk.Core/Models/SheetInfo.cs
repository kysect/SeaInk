using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Models
{
    public struct SheetInfo
    {
        public SheetInfo(string spreadsheetId, int sheetId)
        {
            SpreadsheetId = spreadsheetId.ThrowIfNull();
            SheetId = sheetId;
        }

        public string SpreadsheetId { get; }
        public int SheetId { get; }
    }
}