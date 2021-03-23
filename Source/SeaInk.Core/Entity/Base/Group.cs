using System.Collections.Generic;

namespace SeaInk.Core.Entity.Base
{
    public class Group
    {
        public string Name { get; set; }
        public List<User> Members { get; set; }
    }
}