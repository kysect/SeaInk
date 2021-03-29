using System;
using System.Collections.Generic;
using SeaInk.Core.Entities;

namespace SeaInk.Core.Services
{
    public class StudyEntityService
    {
        public List<Subject> GetSubjectsByMentorSystemId(int curatorId)
        {
            throw new NotImplementedException();
        }

        public List<StudyGroup> GetStudyGroupsBySystemSubjectId(string subject)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetStudentsBySystemGroupId(StudyGroup group)
        {
            throw new NotImplementedException();
        }
    }
}