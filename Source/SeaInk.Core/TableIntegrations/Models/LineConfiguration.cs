using Google.Apis.Sheets.v4.Data;
using SeaInk.Core.TableIntegrations.Models.Styles;
using SeaInk.Core.TableIntegrations.Models.Styles.Enums;
using Color = System.Drawing.Color;
using LineStyle = SeaInk.Core.TableIntegrations.Models.Styles.Enums.LineStyle;

namespace SeaInk.Core.TableIntegrations.Models
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
    
    public static class GoogleLineConfigurationExtension
    {
        public static Border ToGoogleBorder(this LineConfiguration configuration)
            => new Border
            {
                Color = configuration.Color.ToGoogleColor(),
                Style = configuration.Style.ToGoogleLineStyle()
            };
    }
}