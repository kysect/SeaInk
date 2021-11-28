using System.Runtime.Serialization;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class MissingStudentComponentException : SeaInkException
    {
        public MissingStudentComponentException()
            : base("Table does not contain a component that represents student") { }

        protected MissingStudentComponentException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}