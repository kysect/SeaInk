using System.Collections.Generic;

namespace SeaInk.Core.Models.Tables.Styles
{
    public class DefaultSheetMarkup : ISheetMarkup
    {
        public ISheetStyle Style { get; set; } = new DefaultSheetStyle();
        public List<(TableIndex index, ICellStyle style)> Special { get; set; } = new();

        public TableIndex StudentsStartIndex { get; set; } = new (0, 1);
        public TableIndex AssignmentStartIndex { get; set; } = new (1, 0);
        public (int width, int height) CellsPerAssignmentTitle { get; set; }
    }
}