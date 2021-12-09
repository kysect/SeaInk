using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Models
{
    public struct CreateSheetResponse
    {
        public CreateSheetResponse(int sheetId, string sheetLink)
        {
            SheetId = sheetId;
            SheetLink = sheetLink.ThrowIfNull();
        }

        public int SheetId { get; }
        public string SheetLink { get; }
    }
}