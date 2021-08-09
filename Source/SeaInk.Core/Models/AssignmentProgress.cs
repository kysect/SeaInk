using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeaInk.Core.Models
{
    [ComplexType]
    public class AssignmentProgress
    {
        public DateTime CompletionDate { get; set; }
        public double Points { get; set; }

        public AssignmentProgress(DateTime date, float points)
        {
            CompletionDate = date;
            Points = points;
        }
    }
}