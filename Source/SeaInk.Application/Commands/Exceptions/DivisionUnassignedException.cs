using SeaInk.Core.Entities;
using SeaInk.Core.Tools;

namespace SeaInk.Application.Commands.Exceptions;

public class DivisionUnassignedException : SeaInkException
{
    public DivisionUnassignedException(StudyStudentGroup studyStudentGroup)
        : base($"{nameof(studyStudentGroup)}: {studyStudentGroup} does not have a division specified") { }
}