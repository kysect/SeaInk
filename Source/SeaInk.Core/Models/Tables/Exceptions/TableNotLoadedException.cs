namespace SeaInk.Core.Models.Tables.Tables.Exceptions
{
    public class TableNotLoadedException : TableException
    {
        public TableNotLoadedException(string message)
            : base($"Table is not loaded {message}")
        {
        }
    }
}