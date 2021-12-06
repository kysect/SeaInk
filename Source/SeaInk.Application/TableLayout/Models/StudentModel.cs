using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Models
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