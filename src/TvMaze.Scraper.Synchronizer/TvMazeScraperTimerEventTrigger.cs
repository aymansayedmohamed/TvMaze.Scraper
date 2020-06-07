using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using TvMaze.Scraper.Infrastructure;
using TvMaze.Scraper.Infrastructure.Models;
using TvMaze.Scraper.Synchronizer.Requests;

namespace TvMaze.Scraper.Synchronizer
{
    public class TvMazeScraperTimerEventTrigger
    {
        private readonly ITvMazeService _tvMazeService;
        private readonly IMediator _mediator;

        public TvMazeScraperTimerEventTrigger(ITvMazeService tvMazeService, IMediator mediator)
        {
            _tvMazeService = tvMazeService;
            _mediator = mediator;
        }

        // now the function run every minute for the test demo purpose, on the production should run every day or every week
        [FunctionName("TvMazeScraperTimerEventTrigger")]
        public async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# TvMaze Scraper Timer Event Trigger function executed at: {DateTime.Now}");
            int currentPage = 0;
            TvShow[] tvShows;
            try
            {
                while ((tvShows = await _tvMazeService.GetTvShowsAsync(currentPage)).Any())
                {
                    var tvShowMassCreateCommand = new TvShowMassCreateCommand() { TvShows = new List<Models.TvShow>() };

                    foreach (var tvShow in tvShows)
                    {
                        var show = new Models.TvShow
                        {
                            Id = tvShow.Id,
                            Name = tvShow.Name,
                            Casts = tvShow.casts.Select(o => new Models.Cast() { Id = o.Id, Birthday = o.Birthday, Name = o.Name, TvShowId = tvShow.Id }).ToList(),
                            RawResponse = tvShow.RawResponse
                        };

                        tvShowMassCreateCommand.TvShows.Add(show);
                    }

                    await _mediator.Send(tvShowMassCreateCommand);
                    currentPage++;

                    log.LogInformation($": {currentPage} pages had been synchronized");

                }
            }
            catch (Exception ex)
            {
                log.LogError($"an error occured during retrieve shows for page number: {currentPage} ", ex);
            }
        }
    }
}
