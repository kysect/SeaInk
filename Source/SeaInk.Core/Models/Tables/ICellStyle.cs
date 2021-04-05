using System.Drawing;
using SeaInk.Core.Models.Tables.Enums;
using FontStyle = SeaInk.Core.Models.Tables.Enums.FontStyle;

namespace SeaInk.Core.Models.Tables
{
    public interface ICellStyle
    {
        public double Width { get; set; }
        public double Height { get; set; }
        
        public string HyperLink { get; set; }

        public Color BackgroundColor { get; set; }

        public BorderStyle BorderStyle { get; set; }

        public Alignment VerticalAlignment { get; set; }
        public Alignment HorizontalAlignment { get; set; }

        public string FontName { get; set; }
        public FontStyle FontStyle { get; set; }
        public Color FontColor { get; set; }
        
        
        public TextWrapping TextWrapping { get; set; }
    }
}