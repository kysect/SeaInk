using SeaInk.Core.Entities;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class SpreadsheetNotCreatedException : SeaInkException
    {
        public SpreadsheetNotCreatedException(Division division)
            : base($"Division: {division} does not have a spreadsheet specified") { }
    }
}