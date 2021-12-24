using SeaInk.Core.Tools;

namespace SeaInk.Infrastructure.Integrations.GoogleSheets.Exceptions
{
    public class SheetNotFoundException : SeaInkException
    {
        public SheetNotFoundException(string spreadsheetId, int sheetId)
            : base($"Spreadsheet with id {spreadsheetId} has no sheet with id {sheetId}") { }
    }
}