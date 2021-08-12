using Infrastructure.APIs;
using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using SeaInk.Core.APIs;

namespace Infrastructure.Extensions
{
    public static class AddTestServicesExtension
    {
        public static IServiceCollection AddTestServices(this IServiceCollection collection, string databaseName = "SeaInk")
        {
            collection
                .AddSingleton<IUniversitySystemApi, FakeUniversitySystemApi>()
                .AddDbContext<DatabaseContext>(_ => DatabaseContext.GetTestContext(databaseName));

            return collection;
        }
    }
}
