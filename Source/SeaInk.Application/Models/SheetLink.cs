using SeaInk.Utility.Extensions;

namespace SeaInk.Application.Models
{
    public struct SheetLink
    {
        public SheetLink(string value)
        {
            Value = value.ThrowIfNull();
        }

        public string Value { get; }
    }
}