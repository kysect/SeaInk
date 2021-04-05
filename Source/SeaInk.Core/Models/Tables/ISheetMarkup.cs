using System.Collections.Generic;

namespace SeaInk.Core.Models.Tables
{
    public interface ISheetMarkup
    {
        public ISheetStyle Style { get; set; }
        
        //Используется для специальной пометки ячеек, например "удалённый студент"
        public List<(TableIndex index, ICellStyle style)> Special { get; set; }
        
        public TableIndex StudentsStartIndex { get; set; }
        
        public TableIndex AssignmentStartIndex { get; set; }
        public (int width, int height) CellsPerAssignmentTitle { get; set; }
    }
}