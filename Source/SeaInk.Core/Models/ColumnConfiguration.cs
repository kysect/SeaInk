using Kysect.CentumFramework.Sheets.Models.Indices;
using SeaInk.Core.Entities;

namespace SeaInk.Core.Models
{
    public class ColumnConfiguration
    {
        public ColumnType Type { get; }
        public string DeadLine { get; }
        public string Title { get; }
        public SheetIndexRange Range { get; set; }
        

        public ColumnConfiguration(ColumnType type, string title = null, SheetIndexRange range = null, string deadLine = null)
        {
            Type = type;
            Title = title;
            Range = range;
            DeadLine = deadLine;
        }
    }
}