using SeaInk.Core.Exceptions;

namespace SeaInk.Infrastructure.Exceptions
{
    public class SheetNotFoundException : SeaInkException
    {
        public SheetNotFoundException(string spreadsheetId, int sheetId)
            : base($"Spreadsheet with id {spreadsheetId} has no sheet with id {sheetId}") { }
    }
}