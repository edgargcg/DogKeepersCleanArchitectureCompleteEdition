using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Core.Services;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Infrastructure.Repositories;
using AutoMapper;
using System;
using DogKeepers.Core.Options;
using System.Configuration;
using DogKeepers.Infrastructure.ConnectionStrings;
using DogKeepers.Infrastructure.Filters;
using DogKeepers.Core.Interfaces.Utils;
using DogKeepers.Infrastructure.Utils;

namespace DogKeepers.Server
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
            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add<GlobalExceptionFilter>();
                }
            );

            services.AddScoped<IDogService, DogService>();
            services.AddScoped<IDogRepository, DogRepository>();

            services.AddScoped<IRaceService, RaceService>();
            services.AddScoped<IRaceRepository, RaceRepository>();

            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<ISizeRepository, SizeRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IBaseRepository, BaseRepository>();
            services.AddSingleton<IFileUtil, FileUtil>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<PathOptions>(options => Configuration.GetSection("Paths").Bind(options));
            services.Configure<PaginationOption>(options => Configuration.GetSection("Pagination").Bind(options));
            services.Configure<ConnectionString>(options => Configuration.GetSection("ConnectionsString").Bind(options));

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
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
    }
}
