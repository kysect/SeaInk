using System.Collections.Generic;

namespace SeaInk.Core.Entity.Base
{
    public class Group
    {
        public string Name { get; set; }
        public HashSet<int> StudentIdentifiers { get; set; }
    }
}