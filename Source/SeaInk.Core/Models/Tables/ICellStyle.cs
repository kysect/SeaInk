using System.Drawing;

namespace SeaInk.Core.Models.Tables
{
    public interface ICellStyle
    {
        public double Width { get; set; }
        public double Height { get; set; }
        
        public string HyperLink { get; set; }

        public Color BackgroundColor { get; set; }

        public BorderStyle BorderStyle { get; set; }

        public IStyles.Alignment VerticalAlignment { get; set; }
        public IStyles.Alignment HorizontalAlignment { get; set; }

        public string FontName { get; set; }
        public IStyles.FontStyle FontStyle { get; set; }
        public Color FontColor { get; set; }
        
        
        public IStyles.TextWrapping TextWrapping { get; set; }
    }
}