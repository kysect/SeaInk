using SeaInk.Core.Tools;

namespace SeaInk.Core.Entities.Exceptions
{
    public class NotContainingStudentsException : SeaInkException
    {
        public NotContainingStudentsException(StudentGroup studentGroup)
            : base($"{nameof(StudentGroup)}: {studentGroup} does not contain some of the specified students") { }
    }
}