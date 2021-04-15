using System;

namespace SeaInk.Core.Models.Tables.Enums
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
}