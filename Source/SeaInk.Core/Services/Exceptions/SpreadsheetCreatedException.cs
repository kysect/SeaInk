using SeaInk.Core.Entities;
using SeaInk.Core.Tools;

namespace SeaInk.Core.Services.Exceptions
{
    public class SpreadsheetCreatedException : SeaInkException
    {
        public SpreadsheetCreatedException(SubjectDivision subjectDivision)
            : base($"Division: {subjectDivision} already have spreadsheet specified") { }
    }
}