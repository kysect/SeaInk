using IdentityServer4.EntityFramework.Options;
using Infrastructure.Database;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Server.Authorization
{
    public class IdentityDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public IdentityDbContext(
            DbContextOptions<IdentityDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public void SeedUsers(UserManager<IdentityUser> userManager, DatabaseContext databaseContext)
        {
            foreach (Mentor mentor in databaseContext.Mentors)
            {
                var user = new IdentityUser(mentor.LastName)
                {
                    Id = mentor.UniversityId.ToString(),
                    Email = "admin@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "1234567890",
                };

                IdentityResult result = userManager.CreateAsync(user, mentor.UniversityId.ToString()).Result;
            }
        }
    }
}