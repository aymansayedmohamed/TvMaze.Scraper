using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Scraper.Infrastructure.Http.Core;
using TvMaze.Scraper.Infrastructure.Http.Models;
using TvMaze.Scraper.Infrastructure.Http.Responses;

namespace TvMaze.Scraper.Infrastructure.Http
{
    public class TvMazeApi : HttpClientBase, ITvMazeApi
    {
        public TvMazeApi(HttpClient httpClient, ILogger<TvMazeApi> logger)
            : base(httpClient, logger)
        {
        }

        public async Task<CastResponse> GetCastsAsync(int showId)
        {
            var response = await Get<IEnumerable<Cast>>($"/shows/{showId}/cast");
            return new CastResponse() { RawResponse = response.Raw, Casts = response.Result };

        }

        public async Task<TvShowsResponse> GetTvShowsAsync(int pageNumber)
        {
            var response = await Get<IEnumerable<TvShow>>($"/shows?page={pageNumber}");

            return new TvShowsResponse() { RawResponse = response.Raw, TvShows = response.Result.ToArray()};
        }

     
    }
}
