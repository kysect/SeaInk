using System;

namespace SeaInk.Core.TableIntegrations.Models.Styles.Enums
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public static class GoogleDirectionExtension
    {
        public static string ToGoogleDimension(this Direction direction)
            => direction switch
            {
                Direction.Horizontal => "ROWS",
                Direction.Vertical => "COLUMNS",
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
    }
}