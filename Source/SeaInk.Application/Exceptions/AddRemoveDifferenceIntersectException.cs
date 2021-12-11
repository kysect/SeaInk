using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class AddRemoveDifferenceIntersectException<T> : SeaInkException
    {
        public AddRemoveDifferenceIntersectException()
            : base($"The difference between removing and adding lists of type {typeof(T).Name} intersecting") { }
    }
}