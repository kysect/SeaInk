using SeaInk.Core.Tools;

namespace SeaInk.Core.Entities.Exceptions
{
    public class ContainingStudyGroupSubjectsException : SeaInkException
    {
        public ContainingStudyGroupSubjectsException(SubjectDivision subjectDivision)
            : base($"{nameof(SubjectDivision)}: {subjectDivision} already contains some of the specified {nameof(StudyStudentGroup)}s") { }
    }
}