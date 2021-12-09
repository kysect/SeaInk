using SeaInk.Core.Entities;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class SpreadsheetCreatedException : SeaInkException
    {
        public SpreadsheetCreatedException(Division division)
            : base($"Division: {division} already have spreadsheet specified") { }
    }
}