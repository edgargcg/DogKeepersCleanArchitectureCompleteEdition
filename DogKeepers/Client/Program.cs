using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using DogKeepers.Client.Options;
using DogKeepers.Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using DogKeepers.Client.Interfaces;
using Blazored.LocalStorage;

namespace DogKeepers.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            ConfigureServices(builder.Services, builder.Configuration);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration) {
            services.AddOptions();

            services.AddScoped(a => {
                return configuration.GetSection("LocalStorage:Jwt").Get<LocalStorageOption>();
            });

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();

            services.AddAuthorizationCore();
            services.AddScoped<JwtAuthenticationProvider>();
            services.AddScoped<AuthenticationStateProvider, JwtAuthenticationProvider>(
                provider =>
                    provider.GetRequiredService<JwtAuthenticationProvider>()
            );
            services.AddScoped<ILoginService, JwtAuthenticationProvider>(
                provider =>
                    provider.GetRequiredService<JwtAuthenticationProvider>()
            );

            services.AddBlazoredLocalStorage(
                config =>
                    config.JsonSerializerOptions.WriteIndented = true
            );

        }

    }
}
