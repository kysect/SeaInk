using SeaInk.Core.Tools;

namespace SeaInk.Core.TableLayout.Exceptions
{
    public class MissingStudentComponentException : SeaInkException
    {
        public MissingStudentComponentException()
            : base("Table does not contain a component that represents student") { }
    }
}