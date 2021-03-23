using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entity
{
    public class Curator : Base.User
    {
        public List<CuratedGroup> CuratedGroups { get; set; }
    }
}