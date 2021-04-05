using System.Drawing;

namespace SeaInk.Core.Models.Tables
{
    public class BorderStyle
    {
        public LineStyle Leading { get; set; } = new();
        public LineStyle Trailing { get; set; } = new();

        public LineStyle Bottom { get; set; } = new();
        public LineStyle Top { get; set; } = new();

        public BorderStyle()
        {
        }

        public BorderStyle(LineStyleCreator creator)
        {
            Leading = creator.Create();
            Trailing = creator.Create();

            Bottom = creator.Create();
            Top = creator.Create();
        }

        public BorderStyle(LineStyle leading, LineStyle trailing, LineStyle bottom, LineStyle top)
        {
            Leading = leading;
            Trailing = trailing;

            Bottom = bottom;
            Top = top;
        }

        public BorderStyle(IStyles.LineStyle leading, IStyles.LineStyle trailing, IStyles.LineStyle bottom,
            IStyles.LineStyle top)
        {
            Leading = new LineStyle(Color.Black, leading);
            Trailing = new LineStyle(Color.Black, trailing);

            Bottom = new LineStyle(Color.Black, bottom);
            Top = new LineStyle(Color.Black, bottom);
        }
    }
}