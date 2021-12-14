using SeaInk.Core.Tools;

namespace SeaInk.Core.Entities.Exceptions
{
    public class NotContainingMentorsException : SeaInkException
    {
        public NotContainingMentorsException(StudyStudentGroup studyStudentGroup)
            : base($"{nameof(studyStudentGroup)}: {studyStudentGroup} does not contain some of the specified mentors") { }
    }
}