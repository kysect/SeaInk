using System.ComponentModel.DataAnnotations;

namespace SeaInk.Core.Entity.Base
{
    public class User
    {
        [Key]
        public int Identifier { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}