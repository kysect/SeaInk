using System.IO;

namespace SeaInk.Core.TableIntegrations.Models
{
    public class TableInfo
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public TableInfo(string name, string location = null, string id = null, string description = null)
        {
            Name = name;
            Location = location;
            Id = id;
            Description = description;
        }

        public string GetFullPath()
        {
            if (Location is null) return Name;
            return Path.Combine(Location, Name);
        }
    }
}