using SeaInk.Core.Entities;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class SheetCreatedException : SeaInkException
    {
        public SheetCreatedException(StudyGroupSubject studyGroupSubject)
            : base($"StudyGroupSubject: {studyGroupSubject} already have sheet specified") { }
    }
}