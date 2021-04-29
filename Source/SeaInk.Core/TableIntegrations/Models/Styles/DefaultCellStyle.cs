using System.Drawing;
using SeaInk.Core.TableIntegrations.Models.Styles.Enums;
using FontStyle = SeaInk.Core.TableIntegrations.Models.Styles.Enums.FontStyle;

namespace SeaInk.Core.TableIntegrations.Models.Styles
{
    public class DefaultCellStyle : ICellStyle
    {
        public double Width { get; set; } = 100.0;
        public double Height { get; set; } = 50.0;

        public string HyperLink { get; set; }

        public Color BackgroundColor { get; set; } = Color.White;

        public BorderStyle BorderStyle { get; set; } =
            new BorderStyle(new LineStyleCreator(Color.Black, LineStyle.Light));

        public Alignment VerticalAlignment { get; set; } = Alignment.Center;
        public Alignment HorizontalAlignment { get; set; } = Alignment.Center;

        public string FontName { get; set; } = "Arial";
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;
        public Color FontColor { get; set; } = Color.Black;
        public int FontSize { get; set; } = 10;


        public TextWrapping TextWrapping { get; set; } = TextWrapping.NewLine;
    }
}