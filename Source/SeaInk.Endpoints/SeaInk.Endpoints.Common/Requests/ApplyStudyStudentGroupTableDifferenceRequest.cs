using System;
using SeaInk.Core.StudyTable;
using SeaInk.Utility.Extensions;

namespace SeaInk.Endpoints.Common.Requests;

public class ApplyStudyStudentGroupTableDifferenceRequest
{
    public ApplyStudyStudentGroupTableDifferenceRequest(Guid studentStudentGroupId, StudentAssignmentProgressTableDifference difference)
    {
        StudentStudentGroupId = studentStudentGroupId;
        Difference = difference.ThrowIfNull();
    }

    public Guid StudentStudentGroupId { get; }
    public StudentAssignmentProgressTableDifference Difference { get; }
}