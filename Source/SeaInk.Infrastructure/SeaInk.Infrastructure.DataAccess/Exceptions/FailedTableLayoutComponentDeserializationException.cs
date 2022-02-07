using SeaInk.Core.TableLayout;
using SeaInk.Core.Tools;

namespace SeaInk.Infrastructure.DataAccess.Exceptions;

public class FailedTableLayoutComponentDeserializationException : SeaInkException
{
    public FailedTableLayoutComponentDeserializationException(string serialized)
        : base($"{nameof(TableLayoutComponent)} deserialization failed. Value:\n{serialized}") { }
}