using System.Drawing;
using SeaInk.Core.Models.Tables.Tables.Enums;

namespace SeaInk.Core.Models.Tables.Tables
{
    public class BorderStyle
    {
        public LineConfiguration Leading { get; set; } = new();
        public LineConfiguration Trailing { get; set; } = new();

        public LineConfiguration Bottom { get; set; } = new();
        public LineConfiguration Top { get; set; } = new();

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

        public BorderStyle(LineConfiguration leading, LineConfiguration trailing, LineConfiguration bottom, LineConfiguration top)
        {
            Leading = leading;
            Trailing = trailing;

            Bottom = bottom;
            Top = top;
        }

        public BorderStyle(LineStyle leading, LineStyle trailing, LineStyle bottom,
            LineStyle top)
        {
            Leading = new LineConfiguration(Color.Black, leading);
            Trailing = new LineConfiguration(Color.Black, trailing);

            Bottom = new LineConfiguration(Color.Black, bottom);
            Top = new LineConfiguration(Color.Black, top);
        }
    }
}