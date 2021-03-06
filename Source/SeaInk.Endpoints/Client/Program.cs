using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using SeaInk.Endpoints.Sdk;
using MudBlazor.Services;

namespace SeaInk.Endpoints.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazorise(options => { options.ChangeTextOnKeyPress = true; })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddSingleton(_ => new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            RegisterApiClients(builder);

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }

        private static void RegisterApiClients(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<MentorClient>();
            builder.Services.AddScoped<SubjectClient>();
        }
    }
}