using System.Drawing;
using SeaInk.Core.Models.Tables.Enums;
using FontStyle = SeaInk.Core.Models.Tables.Enums.FontStyle;

namespace SeaInk.Core.Models.Tables.Styles
{
    public class DefaultCellStyle : ICellStyle
    {
        public double Width { get; set; } = 100.0;
        public double Height { get; set; } = 50.0;

        public string HyperLink { get; set; } = "";

        public Color BackgroundColor { get; set; } = Color.White;

        public BorderStyle BorderStyle { get; set; } =
            new (new LineStyleCreator(Color.Black, LineStyle.Light));

        public Alignment VerticalAlignment { get; set; } = Alignment.Center;
        public Alignment HorizontalAlignment { get; set; } = Alignment.Center;

        public string FontName { get; set; } = "Arial";
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;
        public Color FontColor { get; set; } = Color.Black;


        public TextWrapping TextWrapping { get; set; } = TextWrapping.NewLine;
    }
}