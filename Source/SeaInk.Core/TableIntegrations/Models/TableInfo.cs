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
        {
            Name = name;
            Location = location;
            Id = id;
            Description = description;
        }

        public TableInfo(string name, string description)
            : this(name, null, null, description) { }
        
        public TableInfo(string name)
            : this(name, null, null, null) { }

        public string GetFullPath()
        {
            if (Location is null) return Name;
            return Path.Combine(Location, Name);
        }
    }
}