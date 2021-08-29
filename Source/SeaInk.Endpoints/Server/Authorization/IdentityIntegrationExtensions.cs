using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SeaInk.Endpoints.Server.Authorization
{
    public static class IdentityIntegrationExtensions
    {
        public static IServiceCollection ConfigureIdentityFramework(this IServiceCollection services)
        {
            services
                .AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("identity_db"))
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>();

            services
                .AddAuthentication()
                .AddIdentityServerJwt();

            services.AddIdentityServer()
                .AddApiAuthorization<IdentityUser, IdentityDbContext>();

            //TODO: reconfig later
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
            });

            return services;
        }

        public static IApplicationBuilder ConfigureIdentityFramework(this IApplicationBuilder app)
        {
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}