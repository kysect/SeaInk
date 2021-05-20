namespace SeaInk.Core.TableIntegrations.Models.Exceptions
{
    public class TableNotLoadedException : TableException
    {
        private const string BaseMessage = "Table is not loaded";
        
        public TableNotLoadedException(string message)
            : base($"{BaseMessage} {message}") { }

        public TableNotLoadedException()
            : base(BaseMessage) { }
    }
}