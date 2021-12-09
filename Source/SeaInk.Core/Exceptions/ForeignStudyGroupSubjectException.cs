using SeaInk.Core.Entities;

namespace SeaInk.Core.Exceptions
{
    public class ForeignStudyGroupSubjectException : SeaInkException
    {
        public ForeignStudyGroupSubjectException(Division division, StudyGroupSubject studyGroupSubject)
            : base($"Division: {division} does not contain StudyGroupSubject: {studyGroupSubject}") { }
    }
}