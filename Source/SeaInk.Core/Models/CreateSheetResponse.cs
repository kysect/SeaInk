using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Models
{
    public struct CreateSheetResponse
    {
        public CreateSheetResponse(int sheetId, SheetLink sheetLink)
        {
            SheetId = sheetId;
            SheetLink = sheetLink.ThrowIfNull();
        }

        public int SheetId { get; }
        public SheetLink SheetLink { get; }
    }
}