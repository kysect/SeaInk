using SeaInk.Core.Tools;

namespace SeaInk.Core.Entities.Exceptions
{
    public class ContainingAssignmentsException : SeaInkException
    {
        public ContainingAssignmentsException(Subject subjectAssignments)
            : base($"{nameof(Subject)}: {subjectAssignments} already contains some of specified assignments") { }
    }
}