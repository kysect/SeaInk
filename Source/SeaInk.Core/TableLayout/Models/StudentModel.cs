using SeaInk.Utility.Extensions;

namespace SeaInk.Core.TableLayout.Models
{
    public class StudentModel
    {
        public StudentModel(string name)
        {
            Name = name.ThrowIfNull();
        }

        public string Name { get; }
    }
}