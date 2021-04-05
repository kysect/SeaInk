using System.Drawing;

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
                IStyles.LineStyle.Bold, 
                IStyles.LineStyle.Light, 
                IStyles.LineStyle.Bold,
                IStyles.LineStyle.Light)
        };

        public ICellStyle AssignmentHeaderCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Bold,
                IStyles.LineStyle.Light)
        };

        public ICellStyle AssignmentHeaderClosingCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Black,
                IStyles.LineStyle.Bold,
                IStyles.LineStyle.Light)
        };


        public ICellStyle StudentListOpeningCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Bold,
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Bold)
        };

        public ICellStyle StudentListCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Bold,
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Light)
        };

        public ICellStyle StudentListClosingCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Bold,
                IStyles.LineStyle.Bold,
                IStyles.LineStyle.Bold)
        };


        public ICellStyle BottomCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Black,
                IStyles.LineStyle.Light)
        };

        public ICellStyle TrailingCellStyle { get; set; } = new DefaultCellStyle
        {
            BorderStyle = new BorderStyle(
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Bold,
                IStyles.LineStyle.Light,
                IStyles.LineStyle.Light)
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
            new BorderStyle(new LineStyleCreator(Color.Black, IStyles.LineStyle.Light));

        public IStyles.Alignment VerticalAlignment { get; set; } = IStyles.Alignment.Center;
        public IStyles.Alignment HorizontalAlignment { get; set; } = IStyles.Alignment.Center;

        public string FontName { get; set; } = "Arial";
        public IStyles.FontStyle FontStyle { get; set; } = IStyles.FontStyle.Regular;
        public Color FontColor { get; set; } = Color.Black;


        public IStyles.TextWrapping TextWrapping { get; set; } = IStyles.TextWrapping.NewLine;
    }
}