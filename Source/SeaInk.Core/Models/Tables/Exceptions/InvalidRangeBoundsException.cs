namespace SeaInk.Core.Models.Tables.Exceptions
{
    public class InvalidRangeBoundsException : TableException
    {
        public InvalidRangeBoundsException(string message)
            : base($"Cannot create range with given bounds {message}")
        {
        }

        public InvalidRangeBoundsException()
            : this("")
        {
        }
    }
}