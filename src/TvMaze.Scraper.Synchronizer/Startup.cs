using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Scraper.Infrastructure.Http;
using TvMaze.Scraper.Infrastructure;
using TvMaze.Scraper.Synchronizer.Extentions;
using TvMaze.Scraper.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

[assembly: FunctionsStartup(typeof(TvMaze.Scraper.Synchronizer.Startup))]
namespace TvMaze.Scraper.Synchronizer
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<TvMazeDbContext>(cfg =>
            {
                cfg.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:TvMaze"), options => options.MigrationsAssembly("TvMaze.Scraper.Synchronizer"));
            });

            //_dbContext.Database.Migrate();

            builder.Services.AddMediatR(typeof(Startup));
            builder.Services.AddTransient<ITvMazeService, TvMazeService>();
            builder.Services.SetupHttpClient<ITvMazeApi, TvMazeApi>("TvMazeApi", configuration: config => config.BaseAddress = new Uri(Environment.GetEnvironmentVariable("TvMazeApiUrl")));

        }

    }
}
