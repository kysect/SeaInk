using SeaInk.Core.Entities;

namespace SeaInk.Core
{
    public interface IUniversitySystemApi
    {
        UniversitySystemUser GetUserBySystemId(int userId);
        Student GetStudentBySystemId(int studentId);
        Mentor GetMentorBySystemId(int mentorId);
        StudyGroup GetStudyGroupBySystemId(int groupId);
        StudyGroup GetStudyGroupByStudentSystemId(int studentId);
        Subject GetSubjectBySystemId(int subjectId);
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