using System;
using SeaInk.Core.TableLayout;

namespace SeaInk.Endpoints.Common.Requests;

public class CreateStudyStudentGroupTableRequest
{
    public CreateStudyStudentGroupTableRequest(Guid studyStudentGroupId, TableLayoutComponent layout)
    {
        StudyStudentGroupId = studyStudentGroupId;
        Layout = layout;
    }

    public Guid StudyStudentGroupId { get; }
    public TableLayoutComponent Layout { get; }
}