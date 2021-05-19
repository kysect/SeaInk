namespace SeaInk.Core.TableIntegrations.Models.Exceptions
{
    public class NonExistingIndexException : TableException
    {
        private const string BaseMessage = "Specified index does not exists";        
        
        public NonExistingIndexException(string message)
            : base($"{BaseMessage} {message}") { }

        public NonExistingIndexException()
            : base(BaseMessage) { }
    }
}