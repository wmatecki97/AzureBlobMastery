using ExampleAzureFunctionProject;
using ExampleAzureFunctionProject.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: WebJobsStartup(typeof(Startup))]
namespace ExampleAzureFunctionProject
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("AzureSqlDatabase");

            builder.Services.AddDbContext<AzureTangyDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.BuildServiceProvider();
        }
    }
}
