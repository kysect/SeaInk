using System;
using ClosedXML.Excel;

namespace SeaInk.Core.TableIntegrations.Models.Styles.Enums
{
    public enum Alignment
    {
        Left,
        Center,
        Right,
        Top,
        Bottom
    }
    
    public static class GoogleAlignmentExtension
    {
        public static string ToGoogleHorizontalAlignment(this Alignment alignment)
            => alignment switch
            {
                Alignment.Left => "LEFT",
                Alignment.Center => "CENTER",
                Alignment.Right => "RIGHT",
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
            };

        public static string ToGoogleVerticalAlignment(this Alignment alignment)
            => alignment switch
            {
                Alignment.Top => "TOP",
                Alignment.Center => "MIDDLE",
                Alignment.Bottom => "BOTTOM",
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
            };
    }

    public static class ExcelAlignmentExtension
    {
        public static XLAlignmentHorizontalValues ToExcelHorizontalAlignment(this Alignment alignment)
            => alignment switch
            {
                Alignment.Left => XLAlignmentHorizontalValues.Left,
                Alignment.Center => XLAlignmentHorizontalValues.Center,
                Alignment.Right => XLAlignmentHorizontalValues.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
            };

        public static XLAlignmentVerticalValues ToExcelVerticalAlignment(this Alignment alignment)
            => alignment switch
            {
                Alignment.Top => XLAlignmentVerticalValues.Top,
                Alignment.Center => XLAlignmentVerticalValues.Center,
                Alignment.Bottom => XLAlignmentVerticalValues.Bottom,
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
            };
    }
}