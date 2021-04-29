namespace SeaInk.Core.TableIntegrations.Models
{
    public class TableInfo
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public TableInfo(string name, string id = null, string description = null)
        {
            Name = name;
            Id = id;
            Description = description;
        }
    }
}