using SeaInk.Core.Entities;
using SeaInk.Core.Tools;

namespace SeaInk.Core.Services.Exceptions
{
    public class SpreadsheetNotCreatedException : SeaInkException
    {
        public SpreadsheetNotCreatedException(SubjectDivision subjectDivision)
            : base($"Division: {subjectDivision} does not have a spreadsheet specified") { }
    }
}