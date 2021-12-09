namespace SeaInk.Application.Models
{
    public struct CreateSpreadsheetResponse
    {
        public CreateSpreadsheetResponse(SheetInfo sheetInfo, SheetLink sheetLink)
        {
            SheetInfo = sheetInfo;
            SheetLink = sheetLink;
        }

        public SheetInfo SheetInfo { get; }
        public SheetLink SheetLink { get; }
    }
}