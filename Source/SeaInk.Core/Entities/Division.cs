using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SeaInk.Core.Entities
{
    public class Division
    {
        public Subject Subject { get; set; }
        public List<StudyGroup> Groups { get; set; }
        
        public string TableId { get; set; }

        public Division(Subject subject, List<StudyGroup> groups, string tableId)
        {
            Subject = subject;
            Groups = groups;
            TableId = tableId;
        }
    }
}