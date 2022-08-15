using SeaInk.Core.Tools;

namespace SeaInk.Core.Entities.Exceptions
{
    public class NotContainingAssignmentsException : SeaInkException
    {
        public NotContainingAssignmentsException(Subject subjectAssignments)
            : base($"{nameof(Subject)}: {subjectAssignments} not contains some of specified assignments") { }
    }
}