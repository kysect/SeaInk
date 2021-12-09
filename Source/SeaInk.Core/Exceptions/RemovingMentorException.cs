using SeaInk.Core.Entities;

namespace SeaInk.Core.Exceptions
{
    public class RemovingMentorException : SeaInkException
    {
        public RemovingMentorException(StudyGroupSubject studyGroupSubject, Mentor mentor)
            : base($"Failed to remove Mentor: {mentor} from StudyGroupSubject{studyGroupSubject}") { }
    }
}