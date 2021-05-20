using System.IO;

namespace SeaInk.Core.TableIntegrations.Models
{
    public class TableInfo
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public TableInfo(string name, string location, string id, string description)
        : this(name, description)
        {
            Location = location;
            Id = id;
        }

        public TableInfo(string name, string description)
            : this(name)
        {
            Description = description;
        }

        public TableInfo(string name)
        {
            Name = name;
        }

        public string GetFullPath()
        {
            if (Location is null) return Name;
            return Path.Combine(Location, Name);
        }
    }
}