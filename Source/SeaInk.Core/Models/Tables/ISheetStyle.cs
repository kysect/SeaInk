using System.Collections.Generic;

namespace SeaInk.Core.Models.Tables
{
    public interface ISheetStyle
    {
        //Индексы закреплённых строк и столбцов
        public (int horizontal, int vertical) Pinned { get; set; }
        public bool IsHidden { get; set; }
        
        public ICellStyle PlaceholderCellStyle { get; set; }
        public ICellStyle DiscardCellStyle { get; set; }
        
        // Стиль открывающих и закрывающих ячеек может отличаться 
        // Ex: см. DefaultSheetStyle
        public ICellStyle AssignmentHeaderOpeningCellStyle { get; set; }
        public ICellStyle AssignmentHeaderCellStyle { get; set; }
        public ICellStyle AssignmentHeaderClosingCellStyle { get; set; }
        
        public ICellStyle StudentListOpeningCellStyle { get; set; }
        public ICellStyle StudentListCellStyle { get; set; }
        public ICellStyle StudentListClosingCellStyle { get; set; }
        
        public ICellStyle BottomCellStyle { get; set; }
        public ICellStyle TrailingCellStyle { get; set; }
        public ICellStyle CommonCellStyle { get; set; }
    }
}