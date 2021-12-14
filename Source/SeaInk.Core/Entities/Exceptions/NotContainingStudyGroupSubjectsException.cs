using SeaInk.Core.Tools;

namespace SeaInk.Core.Entities.Exceptions
{
    public class NotContainingStudyGroupSubjectsException : SeaInkException
    {
        public NotContainingStudyGroupSubjectsException(SubjectDivision subjectDivision)
            : base($"{nameof(SubjectDivision)}: {subjectDivision} does not contain some of the specified {nameof(StudyStudentGroup)}s") { }
    }
}