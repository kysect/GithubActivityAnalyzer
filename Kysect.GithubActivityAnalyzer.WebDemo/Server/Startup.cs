using Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Kysect.GithubActivityAnalyzer.WebDemo.Server.Services;
using Kysect.GithubActivityAnalyzer.ApiAccessor;
using Kysect.GithubActivityAnalyzer.Extensions.Data.Repositories;
using Kysect.GithubActivityAnalyzer.Extensions.Data.Contexts;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();


            services.AddCors(options =>
            {
                options.AddPolicy(name: "KysectCores",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            services.AddDbContext<ActivityContext>(options =>
            {
                options.UseSqlite($"Data Source= ActivityDB.db");
            });
            services.AddDbContext<TeamContext>(options =>
            {
                options.UseSqlite($"Data Source= Teams.db");
            });
            services.AddScoped<DbContext, ActivityContext>();
            services.AddScoped<DbContext, TeamContext>();
        
            services.AddScoped<UserCacheRepository>();
            services.AddScoped<TeamRepository>();
            services.AddScoped<GithubActivityProvider>();

            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddControllers();
        }

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors("KysectCores");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
