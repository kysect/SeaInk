using System.Collections.Generic;
using SeaInk.Core.TableIntegrations.Models;

namespace SeaInk.Core.Entities
{
    public class Division
    {
        public Subject Subject { get; set; }
        public List<StudyGroup> Groups { get; set; }

        public TableInfo TableInfo { get; set; }

        public Division()
            : this(new Subject(), new List<StudyGroup>(), null) { }

        public Division(Subject subject, List<StudyGroup> groups, TableInfo tableInfo)
        {
            Subject = subject;
            Groups = groups;
            TableInfo = tableInfo;
        }
    }
}