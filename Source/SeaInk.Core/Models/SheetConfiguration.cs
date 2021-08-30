using System.Collections.Generic;

namespace SeaInk.Core.Models
{
    public class SheetConfiguration
    {
        public string Title { get; }
        public IReadOnlyList<ColumnConfiguration> Columns { get; }

        public SheetConfiguration(string title, IReadOnlyList<ColumnConfiguration> columns)
        {
            Title = title;
            Columns = columns;
        }
    }
}