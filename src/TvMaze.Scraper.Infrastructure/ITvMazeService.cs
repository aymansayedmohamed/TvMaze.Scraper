using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Scraper.Infrastructure.Http.Responses;
using TvMaze.Scraper.Infrastructure.Models;

namespace TvMaze.Scraper.Infrastructure
{
    public interface ITvMazeService
    {
        Task<TvShow[]> GetTvShowsAsync(int pageNumber);
    }
}
