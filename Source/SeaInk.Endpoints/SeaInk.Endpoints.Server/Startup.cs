using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeaInk.Application.Extensions;
using SeaInk.Core.TableLayout.ComponentsBase;
using SeaInk.Infrastructure.DataAccess.Serialization;
using SeaInk.Utility.Tools;
using AssemblyScanner = SeaInk.Utility.Tools.AssemblyScanner;

namespace SeaInk.Endpoints.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var typeLocator = new TypeLocator();
            typeLocator.AddTypes(AssemblyScanner.ScanAssignableTo<LayoutComponent>(typeof(Core.IAssemblyMarker)));

            services.AddSingleton(typeLocator);
            services.AddSingleton<LayoutComponentSerializationConfiguration>();

            services.AddMediatR(typeof(Startup));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddSwaggerGen();
            services.AddMvc();

            services.AddSeaInkServices(Configuration, builder =>
            {
                builder.UseInMemoryDatabase("SeaInk.db");
                builder.UseLazyLoadingProxies();
                builder.EnableSensitiveDataLogging();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}