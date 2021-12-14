using SeaInk.Core.Tools;

namespace SeaInk.Core.StudyTable.Exceptions
{
    public class AddRemoveDifferenceIntersectException<T> : SeaInkException
    {
        public AddRemoveDifferenceIntersectException()
            : base($"The difference between removing and adding lists of type {typeof(T).Name} intersecting") { }
    }
}