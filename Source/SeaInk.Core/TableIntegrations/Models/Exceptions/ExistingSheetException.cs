namespace SeaInk.Core.TableIntegrations.Models.Exceptions
{
    public class ExistingSheetException : TableException
    {
        public ExistingSheetException(string message)
            : base($"Sheet with specified signature already exists {message}") { }

        public ExistingSheetException()
            : this("") { }
    }
}