using SeaInk.Utility.Extensions;

namespace SeaInk.Core.Entities
{
    public class Student : User
    {
        public Student(int universityId, string firstName, string lastName, string middleName, StudyGroup group)
            : base(universityId, firstName, lastName, middleName)
        {
            Group = group.ThrowIfNull(nameof(group));
        }

        public StudyGroup Group { get; set; }
    }
}