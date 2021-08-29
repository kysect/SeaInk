using IdentityServer4.EntityFramework.Options;
using Infrastructure.Database;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
        }
    }
}