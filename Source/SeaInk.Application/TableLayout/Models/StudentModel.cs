using SeaInk.Utility.Extensions;

namespace SeaInk.Application.TableLayout.Models
{
    public class StudentModel
    {
        public StudentModel(string name)
        {
            Name = name.ThrowIfNull(nameof(name));
        }

        public string Name { get; }
    }
}