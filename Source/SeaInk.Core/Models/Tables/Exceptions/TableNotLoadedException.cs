namespace SeaInk.Core.Models.Tables.Exceptions
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