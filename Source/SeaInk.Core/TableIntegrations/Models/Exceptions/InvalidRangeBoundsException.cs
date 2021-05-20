namespace SeaInk.Core.TableIntegrations.Models.Exceptions
{
    public class InvalidRangeBoundsException : TableException
    {
        private const string BaseMessage = "Cannot create range with given bounds";

        public InvalidRangeBoundsException(string message)
            : base($"{BaseMessage} {message}") { }

        public InvalidRangeBoundsException()
            : base(BaseMessage) { }
    }
}