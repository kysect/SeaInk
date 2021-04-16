using Google.Apis.Sheets.v4.Data;
using Color = System.Drawing.Color;
using LineStyle = SeaInk.Core.Models.Tables.Enums.LineStyle;

namespace SeaInk.Core.Models.Tables
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

        public BorderStyle(LineConfiguration leading, LineConfiguration trailing, LineConfiguration bottom,
            LineConfiguration top)
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

    public static class GoogleBorderStyleExtension
    {
        public static Borders ToGoogleBorder(this BorderStyle style)
            => new Borders
            {
               Top = style.Top.ToGoogleBorder(),
               Bottom = style.Bottom.ToGoogleBorder(),
               Left = style.Leading.ToGoogleBorder(),
               Right = style.Trailing.ToGoogleBorder()
            };
    }
}