using System.Drawing;
using SeaInk.Core.Models.Tables.Tables.Enums;

namespace SeaInk.Core.Models.Tables.Tables
{
    public class LineConfiguration
    {
        public Color Color { get; set; } = Color.Black;
        public LineStyle Style { get; set; } = LineStyle.Light;

        public LineConfiguration()
        {
        }

        public LineConfiguration(LineStyle style)
        {
            Color = Color.Black;
            Style = style;
        }
        public LineConfiguration(Color color, LineStyle style)
        {
            Color = color;
            Style = style;
        }
    }

    public class LineStyleCreator
    {
        private Color Color { get; }
        private LineStyle LineStyle { get; }

        public LineStyleCreator(Color color, LineStyle lineStyle)
        {
            Color = color;
            LineStyle = lineStyle;
        }

        public LineConfiguration Create()
        {
            return new LineConfiguration(Color, LineStyle); 
        }
    }
}