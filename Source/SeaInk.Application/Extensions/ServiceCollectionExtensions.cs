using System;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeaInk.Application.Services;
using SeaInk.Application.Tools;
using SeaInk.Core.Services;
using SeaInk.Infrastructure.DataAccess.Database;
using SeaInk.Infrastructure.Integrations.GoogleSheets;
using SeaInk.Infrastructure.Integrations.UniversitySystem;

namespace SeaInk.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeaInkServices(
            this IServiceCollection services, IConfiguration configuration, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddDbContext<DatabaseContext>(optionsAction);
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<ISheetsService, GoogleSheetsService>();
            services.AddScoped<IUniversityService, FakeUniversityService>();
            services.AddScoped<ITableDifferenceService, TableDifferenceService>();
            services.AddScoped<IDatabaseSynchronizationService, DatabaseSynchronizationService>();
            services.AddScoped<IIdentityService, FakeIdentityService>();
            services.AddMediatR(typeof(IAssemblyMarker));
            AddSheetsService(services, configuration);

            return services;
        }

        private static void AddSheetsService(IServiceCollection collection, IConfiguration configuration)
        {
            string credentialsPath = configuration["GoogleCredentialsPath"];
            string tokenSavePath = configuration["GoogleTokenPath"];

            using var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read);
            ClientSecrets secrets = GoogleClientSecrets.FromStream(stream).Secrets;
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets,
                    new[] { "https://www.googleapis.com/auth/spreadsheets" },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(tokenSavePath, true))
                .GetAwaiter().GetResult();

            var initializer = new BaseClientService.Initializer
            {
                ApplicationName = "SeaInk",
                HttpClientInitializer = credential,
            };

            var sheetsService = new SheetsService(initializer);

            collection.AddScoped(_ => sheetsService);
        }
    }
}