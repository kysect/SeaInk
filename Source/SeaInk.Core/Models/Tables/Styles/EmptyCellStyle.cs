using System.Drawing;
using SeaInk.Core.Models.Tables.Enums;
using FontStyle = SeaInk.Core.Models.Tables.Enums.FontStyle;

namespace SeaInk.Core.Models.Tables.Styles
{
    public class EmptyCellStyle : ICellStyle
    {
        public double Width { get; set; } = 100;
        public double Height { get; set; } = 50;
        public string HyperLink { get; set; }
        public Color BackgroundColor { get; set; } = Color.White;

        public BorderStyle BorderStyle { get; set; } = new()
        {
            Bottom = new LineConfiguration(Color.Black, LineStyle.None),
            Leading = new LineConfiguration(Color.Black, LineStyle.None),
            Top = new LineConfiguration(Color.Black, LineStyle.None),
            Trailing = new LineConfiguration(Color.Black, LineStyle.None)
        };

        public Alignment VerticalAlignment { get; set; } = Alignment.Leading;
        public Alignment HorizontalAlignment { get; set; } = Alignment.Bottom;
        public string FontName { get; set; } = "Arial";
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;
        public Color FontColor { get; set; } = Color.Black;
        public int FontSize { get; set; } = 14;
        public TextWrapping TextWrapping { get; set; } = TextWrapping.Cut;
    }
}