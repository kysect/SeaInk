using System;

namespace SeaInk.Core.TableIntegrations.Models.Styles.Enums
{
    public enum TextWrapping
    {
        /// <summary>
        /// Overlays the contents of other cells
        /// </summary>
        Overlay,

        /// <summary>
        /// Moving words onto the new line
        /// </summary>
        NewLine,

        /// <summary>
        /// Cuts the outstanding content of that cell
        /// </summary>
        Cut
    }

    public static class GoogleTextWrappingExtension
    {
        public static string ToGoogleTextWrapping(this TextWrapping wrapping)
            => wrapping switch
            {
                TextWrapping.Overlay => "OVERFLOW_CELL",
                TextWrapping.NewLine => "WRAP",
                TextWrapping.Cut => "CLIP",
                _ => throw new ArgumentOutOfRangeException(nameof(wrapping), wrapping, null)
            };
    }
    
    public static class ExcelTextWrappingExtension
    {
        public static bool ToExcelTextWrapping(this TextWrapping wrapping)
            => wrapping switch
            {
                TextWrapping.NewLine => true,
                TextWrapping.Cut => false,
                TextWrapping.Overlay => false,
                _ => throw new ArgumentOutOfRangeException(nameof(wrapping), wrapping, null)
            };
    }
}