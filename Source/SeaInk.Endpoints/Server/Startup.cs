using System;
using System.Linq;
using Infrastructure.APIs;
using Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeaInk.Core.Entities;

namespace SeaInk.Endpoints.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(builder => DatabaseContext.ConfigureTestBuilder(builder, "SeaInk"));
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext databaseContext)
        {
            if (env.IsDevelopment())
            {
                AddTestData(databaseContext);
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }

        public static void AddTestData(DatabaseContext databaseContext)
        {
            var api = new FakeUniversitySystemApi();
            databaseContext.AddRange(api.Users);
            databaseContext.AddRange(api.Students);
            databaseContext.AddRange(api.Mentors);
            databaseContext.AddRange(api.Groups);
            databaseContext.AddRange(api.Assignments);
            databaseContext.AddRange(api.Subjects);
            databaseContext.AddRange(api.StudentAssignmentProgresses);
            databaseContext.AddRange(api.Divisions);
        }
    }
}