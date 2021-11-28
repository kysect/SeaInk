using System.Runtime.Serialization;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    public class MissingAssignmentComponentException : SeaInkException
    {
        public MissingAssignmentComponentException(AssignmentModel model)
            : base($"There is no component in the table that represents an assignment {model}") { }

        protected MissingAssignmentComponentException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}