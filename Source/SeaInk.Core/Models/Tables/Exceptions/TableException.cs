using System;

namespace SeaInk.Core.Models.Tables.Exceptions
{
    /// <summary>
    /// Generic ITable exception, any occured exceptions in ITable or
    /// its subclasses work must be subclassed from it, in order to simplify
    /// specific catching 
    /// </summary>
    public class TableException : Exception
    {
        public TableException(string message = "")
            : base(message ?? "Unspecified table exception occured")
        {
        }
    }
}