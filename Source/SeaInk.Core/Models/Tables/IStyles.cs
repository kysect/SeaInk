namespace SeaInk.Core.Models.Tables
{
    public interface IStyles
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

        public enum Alignment
        {
            Leading,
            Center,
            Trailing,
            Top,
            Bottom
        }

        public enum FontStyle
        {
            Regular,
            Bold,
            Italic,
            Crossed
        }
        
        public enum TextWrapping
        {
            Expand, //Выводить текст за рамки ячейки
            NewLine, //Переносить слова на новую строку
            Cut //Обрезать текст
        }
    }
}