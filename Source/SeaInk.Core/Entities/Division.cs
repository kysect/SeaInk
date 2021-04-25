using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SeaInk.Core.Entities
{
    public class Division
    {
        public Subject Subject { get; set; } = new Subject();
        public List<StudyGroup> Groups { get; set; } = new List<StudyGroup>();

        public string TableId { get; set; } = "";

        public Division()
        {
        }
        
        public Division(Subject subject, List<StudyGroup> groups, string tableId)
        {
            Subject = subject;
            Groups = groups;
            TableId = tableId;
        }
    }
}