namespace SeaInk.Core.TableIntegrations.Models.Exceptions
{
    public class NonExistingIndexException : TableException
    {
        public NonExistingIndexException(string message)
            : base($"Specified index does not exists {message}")
        {
        }

        public NonExistingIndexException()
            : this("")
        {
        }
    }
}