using SeaInk.Core.Entities;

namespace SeaInk.Core.APIs
{
    public interface IUniversitySystemApi
    {
        User GetUser(int id);
        Student GetStudent(int id);
        Mentor GetMentor(int id);
        StudyGroup GetStudyGroup(int id);
        StudyAssignment GetStudyAssignment(int id);
        Subject GetSubject(int id);
        StudentAssignmentProgress GetStudentAssignmentProgress(int studentId, int assignmentId);
        StudyGroupSubject GetStudyGroupSubject(int mentorId, int subjectId);

        void SaveUser(User user);
        void SaveStudent(Student student);
        void SaveMentor(Mentor mentor);
        void SaveStudyGroup(StudyGroup group);
        void SaveStudyAssignment(StudyAssignment assignment);
        void SaveSubject(Subject subject);
        void SaveAssignmentProgress(StudentAssignmentProgress progress);
        void SaveDivision(Division division);
    }
}