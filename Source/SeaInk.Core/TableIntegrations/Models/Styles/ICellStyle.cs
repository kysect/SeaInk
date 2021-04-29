using Google.Apis.Sheets.v4.Data;
using SeaInk.Core.TableIntegrations.Models.Styles.Enums;
using Color = System.Drawing.Color;
using GoogleColor = Google.Apis.Sheets.v4.Data.Color;
using FontStyle = SeaInk.Core.TableIntegrations.Models.Styles.Enums.FontStyle;

namespace SeaInk.Core.TableIntegrations.Models.Styles
{
    public interface ICellStyle
    {
        double Width { get; set; }
        double Height { get; set; }

        string HyperLink { get; set; }

        Color BackgroundColor { get; set; }

        BorderStyle BorderStyle { get; set; }

        Alignment VerticalAlignment { get; set; }
        Alignment HorizontalAlignment { get; set; }

        string FontName { get; set; }
        FontStyle FontStyle { get; set; }
        Color FontColor { get; set; }
        int FontSize { get; set; }


        TextWrapping TextWrapping { get; set; }
    }

    public static class GoogleICellStyleExtension
    {
        public static CellData ToGoogleCellData(this ICellStyle style)
            => new CellData
            {
                UserEnteredFormat = new CellFormat
                {
                    BackgroundColor = style.BackgroundColor.ToGoogleColor(),
                    Borders = style.BorderStyle.ToGoogleBorder(),
                    HorizontalAlignment = style.HorizontalAlignment.ToGoogleHorizontalAlignment(),
                    VerticalAlignment = style.VerticalAlignment.ToGoogleVerticalAlignment(),
                    WrapStrategy = style.TextWrapping.ToGoogleTextWrapping(),
                    TextFormat = new TextFormat
                    {
                        ForegroundColor = style.FontColor.ToGoogleColor(),
                        FontFamily = style.FontName,
                        FontSize = style.FontSize,
                        Bold = style.FontStyle == FontStyle.Bold,
                        Italic = style.FontStyle == FontStyle.Italic,
                        Strikethrough = style.FontStyle == FontStyle.Crossed,
                        Underline = style.FontStyle == FontStyle.Underlined
                    }
                },
                Hyperlink = style.HyperLink
            };

        public static GoogleColor ToGoogleColor(this Color color)
            => new GoogleColor
            {
                Alpha = color.A * 255,
                Red = color.R * 255,
                Green = color.G * 255,
                Blue = color.B * 255
            };
    }
}