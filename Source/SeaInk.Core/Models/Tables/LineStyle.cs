using System.Drawing;

namespace SeaInk.Core.Models.Tables
{
    public class LineStyle
    {
        public Color Color { get; set; } = Color.Black;
        public IStyles.LineStyle Style { get; set; } = IStyles.LineStyle.Light;

        public LineStyle()
        {
        }

        public LineStyle(IStyles.LineStyle style)
        {
            Color = Color.Black;
            Style = style;
        }
        public LineStyle(Color color, IStyles.LineStyle style)
        {
            Color = color;
            Style = style;
        }
    }

    public class LineStyleCreator
    {
        private Color Color { get; }
        private IStyles.LineStyle LineStyle { get; }

        public LineStyleCreator(Color color, IStyles.LineStyle lineStyle)
        {
            Color = color;
            LineStyle = lineStyle;
        }

        public LineStyle Create()
        {
            return new LineStyle(Color, LineStyle); 
        }
    }
}