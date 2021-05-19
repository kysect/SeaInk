namespace SeaInk.Core.TableIntegrations.Models.Exceptions
{
    public class ExistingSheetException : TableException
    {
        private const string BaseMessage = "Sheet with specified signature already exists";

        public ExistingSheetException(string message)
            : base($"{BaseMessage} {message}") { }

        public ExistingSheetException()
            : base(BaseMessage) { }
    }
}