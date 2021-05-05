using Kysect.GithubActivityAnalyzer.Data.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Kysect.GithubActivityAnalyzer.WebDemo.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();
            var activityContext = scope.ServiceProvider.GetService<ActivityContext>();
            activityContext.Database.EnsureCreated(); 
            var studentsGroupContext = scope.ServiceProvider.GetService<TeamContext>();
            studentsGroupContext.Database.EnsureCreated();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
