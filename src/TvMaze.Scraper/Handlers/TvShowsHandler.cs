using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TvMaze.Scraper.Data;
using TvMaze.Scraper.Infrastructure.Http.Responses;
using TvMaze.Scraper.Models;
using TvMaze.Scraper.Requests;
using TvMaze.Scraper.Responses;

namespace TvMaze.Scraper.Handlers
{
    public class TvShowsHandler : IRequestHandler<TvShowsQuery, TvShowResponseModel>
    {
        private readonly TvMazeDbContext _dbContext;

        public TvShowsHandler(TvMazeDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<TvShowResponseModel> Handle(TvShowsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var tvShows = _dbContext.TvShows.OrderBy(o => o.Id).AsNoTracking()
                              .Skip(request.pageSize * (request.pageNumber - 1))
                              .Take(request.pageSize);

            TvShowResponseModel respone = new TvShowResponseModel();
            respone.TvShows = tvShows.Select(o => new TvShow()
            {
                Id = o.Id,
                Name = o.Name,
                Casts = o.TvShowCasts.OrderByDescending(c => c.Cast.Birthday).Select(c => new Cast()
                {
                    Id = c.Cast.Id,
                    Name = c.Cast.Name,
                    Birthday = c.Cast.Birthday
                }).ToArray()
            }).ToArray();

            return respone;

        }
    }
}
