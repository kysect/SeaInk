using System.Collections.Generic;

namespace SeaInk.Core.Models.Tables
{
    public interface ISheetMarkup
    {
        ISheetStyle Style { get; set; }
        
        /// <summary>
        /// Being used for special marking of particular cells, ex: "Deleted" student
        /// </summary>
        List<(TableIndex index, ICellStyle style)> Special { get; set; }
        
        TableIndex StudentsStartIndex { get; set; }
        
        TableIndex AssignmentStartIndex { get; set; }
        (int width, int height) CellsPerAssignmentTitle { get; set; }
    }
}