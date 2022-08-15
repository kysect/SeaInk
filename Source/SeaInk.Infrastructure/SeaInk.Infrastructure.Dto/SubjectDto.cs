namespace SeaInk.Infrastructure.Dto;

public record SubjectDto(Guid Id, int UniversityId, string Name, IReadOnlyCollection<StudyAssignmentDto> Assignments);