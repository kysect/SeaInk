using SeaInk.Core.Entities;
using SeaInk.Core.Tools;

namespace SeaInk.Core.Services.Exceptions
{
    public class SheetCreatedException : SeaInkException
    {
        public SheetCreatedException(StudyStudentGroup studyStudentGroup)
            : base($"StudyGroupSubject: {studyStudentGroup} already have sheet specified") { }
    }
}