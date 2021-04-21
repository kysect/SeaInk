using System.Collections.Generic;
using System.Text.RegularExpressions;
using SeaInk.Core.Models.Tables;

namespace SeaInk.Core.Entities
{
    public class Division
    {
        public Subject Subject { get; set; }
        public List<StudyGroup> Groups { get; set; }

        public TableInfo TableInfo { get; set; }

        public Division()
        {
            Subject = new Subject();
            Groups = new List<StudyGroup>();
            TableInfo = null;
        }
        public Division(Subject subject, List<StudyGroup> groups, TableInfo tableInfo)
        {
            Subject = subject;
            Groups = groups;
            TableInfo = tableInfo;
        }
    }
}