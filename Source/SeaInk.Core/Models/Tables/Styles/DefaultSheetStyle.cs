using System.Drawing;
using SeaInk.Core.Models.Tables.Enums;
using FontStyle = SeaInk.Core.Models.Tables.Enums.FontStyle;

namespace SeaInk.Core.Models.Tables.Styles
{
    public class DefaultSheetStyle : ISheetStyle
    {
        public (int horizontal, int vertical) Pinned { get; set; } = (-1, -1);
        public bool IsHidden { get; set; } = false;

        public ICellStyle PlaceholderCellStyle { get; set; } = new DefaultCellStyle
        {
            BackgroundColor = Color.Black
        };

        public ICellStyle DiscardCellStyle { get; set; } = new DefaultCellStyle
        {
            BackgroundColor = Color.IndianRed
        };

        public ICellStyle AssignmentHeaderOpeningCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                LineStyle.Bold, 
                LineStyle.Light, 
                LineStyle.Bold,
                LineStyle.Light)
        };

        public ICellStyle AssignmentHeaderCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                LineStyle.Light,
                LineStyle.Light,
                LineStyle.Bold,
                LineStyle.Light)
        };

        public ICellStyle AssignmentHeaderClosingCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                LineStyle.Light,
                LineStyle.Black,
                LineStyle.Bold,
                LineStyle.Light)
        };


        public ICellStyle StudentListOpeningCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                LineStyle.Light,
                LineStyle.Bold,
                LineStyle.Light,
                LineStyle.Bold)
        };

        public ICellStyle StudentListCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                LineStyle.Light,
                LineStyle.Bold,
                LineStyle.Light,
                LineStyle.Light)
        };

        public ICellStyle StudentListClosingCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                LineStyle.Light,
                LineStyle.Bold,
                LineStyle.Bold,
                LineStyle.Bold)
        };


        public ICellStyle BottomCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                LineStyle.Light,
                LineStyle.Light,
                LineStyle.Black,
                LineStyle.Light)
        };

        public ICellStyle TrailingCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                LineStyle.Light,
                LineStyle.Bold,
                LineStyle.Light,
                LineStyle.Light)
        };

        public ICellStyle CommonCellStyle { get; set; } = new DefaultCellStyle();
    }

    public class DefaultCellStyle : ICellStyle
    {
        public double Width { get; set; } = 100.0;
        public double Height { get; set; } = 50.0;

        public string HyperLink { get; set; } = "";

        public Color BackgroundColor { get; set; } = Color.White;

        public BorderStyle BorderStyle { get; set; } =
            new BorderStyle(new LineStyleCreator(Color.Black, LineStyle.Light));

        public Alignment VerticalAlignment { get; set; } = Alignment.Center;
        public Alignment HorizontalAlignment { get; set; } = Alignment.Center;

        public string FontName { get; set; } = "Arial";
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;
        public Color FontColor { get; set; } = Color.Black;


        public TextWrapping TextWrapping { get; set; } = TextWrapping.NewLine;
    }
}