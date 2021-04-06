using System.Drawing;
using SeaInk.Core.Models.Tables.Enums;
using FontStyle = SeaInk.Core.Models.Tables.Enums.FontStyle;

namespace SeaInk.Core.Models.Tables
{
    public interface ICellStyle
    {
        double Width { get; set; }
        double Height { get; set; }
        
        string HyperLink { get; set; }

        Color BackgroundColor { get; set; }

        BorderStyle BorderStyle { get; set; }

        Alignment VerticalAlignment { get; set; }
        Alignment HorizontalAlignment { get; set; }

        string FontName { get; set; }
        FontStyle FontStyle { get; set; }
        Color FontColor { get; set; }
        
        
        TextWrapping TextWrapping { get; set; }
    }
}