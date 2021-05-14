using System;
using ClosedXML.Excel;

namespace SeaInk.Core.TableIntegrations.Models.Styles.Enums
{
    public enum LineStyle
    {
        None,
        Light,
        Bold,
        Black,
        Dashed,
        Dotted,
        Doubled
    }
    
    public static class GoogleLineStyleExtension
    {
        public static string ToGoogleLineStyle(this LineStyle style)
            => style switch
            {
                LineStyle.None => "NONE",
                LineStyle.Light => "SOLID",
                LineStyle.Bold => "SOLID_MEDIUM",
                LineStyle.Black => "SOLID_THICK",
                LineStyle.Dashed => "DASHED",
                LineStyle.Dotted => "DOTTED",
                LineStyle.Doubled => "DOUBLE",
                _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
            };
    }

    public static class ExcelLineStyleExtension
    {
        public static XLBorderStyleValues ToExcelLineStyle(this LineStyle style) => style switch
        {
            LineStyle.None => XLBorderStyleValues.None,
            LineStyle.Light => XLBorderStyleValues.Thin,
            LineStyle.Black => XLBorderStyleValues.Thick,
            LineStyle.Dashed => XLBorderStyleValues.Dashed,
            LineStyle.Dotted => XLBorderStyleValues.Dotted,
            LineStyle.Doubled => XLBorderStyleValues.Double,
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };
    }
}