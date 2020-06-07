using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Scraper.Infrastructure.Http.Responses;

namespace TvMaze.Scraper.Infrastructure.Http
{
    public interface ITvMazeApi
    {
        Task<TvShowsResponse> GetTvShowsAsync(int pageNumber);
        Task<CastResponse> GetCastsAsync(int showId);

    }
}
