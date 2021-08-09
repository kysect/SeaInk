using Infrastructure.APIs;
using Infrastructure.Database;
using Infrastructure.Repositories.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SeaInk.Core.APIs;
using SeaInk.Core.Entities;
using SeaInk.Core.Repositories;

namespace Infrastructure.Extensions
{
    public static class AddTestServicesExtension
    {
        public static IServiceCollection AddTestServices(this IServiceCollection collection, string databaseName = "SeaInk")
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;


            collection
                .AddSingleton<IUniversitySystemApi, FakeUniversitySystemApi>()
                .AddSingleton(new DatabaseContext(options))
                .AddSingleton<IEntityRepository<User>, DbUserRepository>()
                .AddSingleton<IEntityRepository<Student>, DbStudentRepository>()
                .AddSingleton<IEntityRepository<Mentor>, DbMentorRepository>()
                .AddSingleton<IEntityRepository<Division>, DbDivisionRepository>()
                .AddSingleton<IEntityRepository<Subject>, DbSubjectRepository>()
                .AddSingleton<IEntityRepository<StudyGroup>, DbStudyGroupRepository>()
                .AddSingleton<IEntityRepository<StudyAssignment>, DbStudyAssignmentRepository>()
                .AddSingleton<IEntityRepository<StudentAssignmentProgress>, DbStudentAssignmentProgressRepository>();

            return collection;
        }
    }
}