//using EFCore.BulkExtensions;
using EFCore.BulkExtensions;
using MediatR;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TvMaze.Scraper.Data;
using TvMaze.Scraper.Data.Entities;
using TvMaze.Scraper.Synchronizer.DistinctComparers;
using TvMaze.Scraper.Synchronizer.Requests;

namespace TvMaze.Scraper.Synchronizer.Handlers
{
    public class TvShowMassCreateHandler : AsyncRequestHandler<TvShowMassCreateCommand>
    {
        private readonly TvMazeDbContext _dbContext;
        private readonly ILogger<TvShowMassCreateHandler> _logger;

        public TvShowMassCreateHandler(TvMazeDbContext dbContext, ILogger<TvShowMassCreateHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;

            // this Migration step should not be here , it should happen during the CI pipline running throw console app that run as a step in the CI tasks.
            //I put it here to just build the database without send a script and make you run it , again this is not the correct place for it 
            _dbContext.Database.Migrate();
        }

        protected override async Task Handle(TvShowMassCreateCommand request, CancellationToken cancellationToken)
        {
            var bulkConfig = new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true };

            var tvShows = new List<TvShow>();
            var casts = new List<Cast>();
            var tvShowCasts = new List<TvShowCast>();
            foreach (var tvShow in request.TvShows)
            {
                var show = new TvShow
                {
                    Id = tvShow.Id,
                    Name = tvShow.Name,
                    RawResponse = tvShow.RawResponse
                };

                tvShows.Add(show);
                casts.AddRange(tvShow.Casts.Select(o => new Cast() { Id = o.Id, Birthday = o.Birthday, Name = o.Name }).ToList());
                tvShowCasts.AddRange(tvShow.Casts.Select(o => new TvShowCast() { CastId = o.Id, TvShowId = tvShow.Id }).ToList());

            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _dbContext.BulkInsertOrUpdateAsync(tvShows, bulkConfig);
                    await _dbContext.BulkInsertOrUpdateAsync(casts.Distinct(new DistinctCastComparer()).ToList(), bulkConfig);
                    await _dbContext.BulkInsertOrUpdateAsync(tvShowCasts.Distinct(new DistinctTvShowCastComparer()).ToList(), bulkConfig);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"an error occured during save the tvshows to the DB ", ex);
                    transaction.Rollback();
                }
            }

        }
    }

}
