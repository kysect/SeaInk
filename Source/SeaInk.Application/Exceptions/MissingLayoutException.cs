using SeaInk.Core.Entities;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class MissingLayoutException : SeaInkException
    {
        public MissingLayoutException(StudyGroupSubject studyGroupSubject)
        : base($"StudyGroupSubject: {studyGroupSubject} has no layout saved") { }
    }
}