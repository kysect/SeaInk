using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SeaInk.Core.Models
{
    [Owned]
    public record AssignmentProgress
    {
        [Column("CompletionDate")]
        public DateTime CompletionDate { get; set; }
        [Column("Points")]
        public double Points { get; set; }
    }
}