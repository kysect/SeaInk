using SeaInk.Core.Tools;

namespace SeaInk.Core.Entities.Exceptions
{
    public class ContainingMentorsException : SeaInkException
    {
        public ContainingMentorsException(StudyStudentGroup studyStudentGroup)
            : base($"{nameof(studyStudentGroup)}: {studyStudentGroup} already contains some of specified mentors") { }
    }
}