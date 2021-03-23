using System;
using System.Collections.Generic;
using SeaInk.Core.Entity;
using SeaInk.Core.Entity.Base;

namespace SeaInk.Core.Services
{
    public class StudyEntityService
    {
        public virtual List<Program> GetSubjectsByCurator(int curatorId)
        {
            throw new NotImplementedException();
        }

        public virtual List<CuratedGroup> GetCuratedGroupsBySubject(string subject)
        {
            throw new NotImplementedException();
        }

        public virtual List<Student> GetStudentsByUniversityGroup(UniversityGroup group)
        {
            throw new NotImplementedException();
        }
    }
}