using System.Collections.Generic;

namespace SeaInk.Core.Models
{
    public class SheetConfiguration
    {
        public string Title { get; }
        public List<ColumnConfiguration> Columns { get; }

        public SheetConfiguration(string title, List<ColumnConfiguration> columns)
        {
            Title = title;
            Columns = columns;
        }
    }
}