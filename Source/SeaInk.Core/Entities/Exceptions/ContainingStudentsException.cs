using SeaInk.Core.Tools;

namespace SeaInk.Core.Entities.Exceptions
{
    public class ContainingStudentsException : SeaInkException
    {
        public ContainingStudentsException(StudentGroup studentGroup)
            : base($"{nameof(StudentGroup)}: {studentGroup} already contains some of the specified students") { }
    }
}