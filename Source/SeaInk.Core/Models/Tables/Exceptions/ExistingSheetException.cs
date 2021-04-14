namespace SeaInk.Core.Models.Tables.Exceptions
{
    public class ExistingSheetException : TableException
    {
        public ExistingSheetException(string message) 
            : base($"Sheet with specified signature already exists {message}")
        {
        }

        public ExistingSheetException()
            : this("")
        {
        }
    }
}