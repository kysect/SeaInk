using System;
using Microsoft.EntityFrameworkCore;

namespace SeaInk.Core.Models
{
    [Owned]
    public record AssignmentProgress(DateTime CompletionDate, double Points);
}