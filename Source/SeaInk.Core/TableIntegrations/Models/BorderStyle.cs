using Google.Apis.Sheets.v4.Data;
using Color = System.Drawing.Color;
using LineStyle = SeaInk.Core.TableIntegrations.Models.Styles.Enums.LineStyle;

namespace SeaInk.Core.TableIntegrations.Models
{
    public class BorderStyle
    {
        public LineConfiguration Left { get; set; } = new LineConfiguration();
        public LineConfiguration Right { get; set; } = new LineConfiguration();

        public LineConfiguration Bottom { get; set; } = new LineConfiguration();
        public LineConfiguration Top { get; set; } = new LineConfiguration();

        public BorderStyle() { }

        public BorderStyle(LineStyleCreator creator)
        {
            Left = creator.Create();
            Right = creator.Create();

            Bottom = creator.Create();
            Top = creator.Create();
        }

        public BorderStyle(LineConfiguration left, LineConfiguration right, LineConfiguration bottom,
            LineConfiguration top)
        {
            Left = left;
            Right = right;

            Bottom = bottom;
            Top = top;
        }

        public BorderStyle(LineStyle leading, LineStyle trailing, LineStyle bottom,
            LineStyle top)
        {
            Left = new LineConfiguration(Color.Black, leading);
            Right = new LineConfiguration(Color.Black, trailing);

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
                Left = style.Left.ToGoogleBorder(),
                Right = style.Right.ToGoogleBorder()
            };
    }
}