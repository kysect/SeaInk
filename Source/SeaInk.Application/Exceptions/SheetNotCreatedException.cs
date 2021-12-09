using SeaInk.Core.Entities;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class SheetNotCreatedException : SeaInkException
    {
        public SheetNotCreatedException(StudyGroupSubject studyGroupSubject)
            : base($"StudyGroupSubject: {studyGroupSubject} does not have a sheet specified") { }
    }
}