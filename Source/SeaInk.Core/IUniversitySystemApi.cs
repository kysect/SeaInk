using System.Collections.Generic;
using SeaInk.Core.Entity;
using SeaInk.Core.Model;

namespace SeaInk.Core
{
    public interface IUniversitySystemApi
    {
        UniversitySystemUser GetUserBySystemId(int userId);
        Student GetStudentBySystemId(int studentId);
        Mentor GetMentorBySystemId(int mentorId);
        StudyGroup GetStudyGroupBySystemId(int groupId);
        Subject GetSubjectBySystemId(int subjectId);
        Division GetDivisionBySystemId(int divisionId);
        StudentAssignmentProgress GetStudentAssignmentProgressByIds(int studentId, int assignmentId);

        void SaveUser(UniversitySystemUser user);
        void SaveStudent(Student student);
        void SaveMentor(Mentor mentor);
        void SaveStudyGroup(StudyGroup group);
        void SaveSubject(Subject subject);
        void SaveDivision(Division division);
        void SaveAssignmentProgress(StudentAssignmentProgress progress);
    }
}