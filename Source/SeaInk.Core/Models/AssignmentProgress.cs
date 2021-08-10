using System;
using Microsoft.EntityFrameworkCore;

namespace SeaInk.Core.Models
{
    [Owned]
    public record AssignmentProgress
    {
        public DateTime CompletionDate { get; set; }
        public double Points { get; set; }
    }
}