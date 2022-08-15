using SeaInk.Core.Entities;
using SeaInk.Core.Tools;

namespace SeaInk.Core.Services.Exceptions
{
    public class SheetNotCreatedException : SeaInkException
    {
        public SheetNotCreatedException(StudyStudentGroup studyStudentGroup)
            : base($"StudyGroupSubject: {studyStudentGroup} does not have a sheet specified") { }
    }
}