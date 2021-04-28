namespace SeaInk.Core.TableIntegrations.Models.Exceptions
{
    public class TableNotLoadedException : TableException
    {
        public TableNotLoadedException(string message)
            : base($"Table is not loaded {message}")
        {
        }

        public TableNotLoadedException()
            : this("")
        {
        }
    }
}