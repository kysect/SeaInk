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
            collection
                .AddSingleton<IUniversitySystemApi, FakeUniversitySystemApi>()
                .AddDbContext<DatabaseContext>(o => o.UseInMemoryDatabase("SeaInk"))
                .AddScoped<IEntityRepository<User>, DbUserRepository>()
                .AddScoped<IEntityRepository<Student>, DbStudentRepository>()
                .AddScoped<IEntityRepository<Mentor>, DbMentorRepository>()
                .AddScoped<IEntityRepository<Division>, DbDivisionRepository>()
                .AddScoped<IEntityRepository<Subject>, DbSubjectRepository>()
                .AddScoped<IEntityRepository<StudyGroup>, DbStudyGroupRepository>()
                .AddScoped<IEntityRepository<StudyAssignment>, DbStudyAssignmentRepository>()
                .AddScoped<IEntityRepository<StudentAssignmentProgress>, DbStudentAssignmentProgressRepository>();

            return collection;
        }
    }
}