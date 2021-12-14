using System;

namespace SeaInk.Endpoints.Common.Requests;

public class CalculateStudyStudentGroupTableDifferenceRequest
{
    public CalculateStudyStudentGroupTableDifferenceRequest(Guid studyStudentGroupId)
    {
        StudyStudentGroupId = studyStudentGroupId;
    }

    public Guid StudyStudentGroupId { get; }
}