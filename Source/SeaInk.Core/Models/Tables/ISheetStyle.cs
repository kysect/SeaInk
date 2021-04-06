using System.Collections.Generic;

namespace SeaInk.Core.Models.Tables
{
    public interface ISheetStyle
    {
        //Индексы закреплённых строк и столбцов
        (int horizontal, int vertical) Pinned { get; set; }
        bool IsHidden { get; set; }
        
        ICellStyle PlaceholderCellStyle { get; set; }
        ICellStyle DiscardCellStyle { get; set; }
        
        // Стиль открывающих и закрывающих ячеек может отличаться 
        // Ex: см. DefaultSheetStyle
        ICellStyle AssignmentHeaderOpeningCellStyle { get; set; }
        ICellStyle AssignmentHeaderCellStyle { get; set; }
        ICellStyle AssignmentHeaderClosingCellStyle { get; set; }
        
        ICellStyle StudentListOpeningCellStyle { get; set; }
        ICellStyle StudentListCellStyle { get; set; }
        ICellStyle StudentListClosingCellStyle { get; set; }
        
        ICellStyle BottomCellStyle { get; set; }
        ICellStyle TrailingCellStyle { get; set; }
        ICellStyle CommonCellStyle { get; set; }
    }
}