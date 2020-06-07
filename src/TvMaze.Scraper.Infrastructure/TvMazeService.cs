using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TvMaze.Scraper.Infrastructure.Http;
using TvMaze.Scraper.Infrastructure.Http.Responses;
using TvMaze.Scraper.Infrastructure.Models;

namespace TvMaze.Scraper.Infrastructure
{
    public class TvMazeService : ITvMazeService
    {
        private readonly ITvMazeApi _tvMazeApi;
        private readonly ILogger<TvMazeService> _logger;
        public TvMazeService(ITvMazeApi tvMazeApi, ILogger<TvMazeService> logger)
        {
            _tvMazeApi = tvMazeApi;
            _logger = logger;
        }

        public async Task<TvShow[]> GetTvShowsAsync(int pageNumber)
        {
            try
            {
                var result = new List<TvShow>();
                var tvShowRespnse = await _tvMazeApi.GetTvShowsAsync(pageNumber);
                int lastSuccedCastCall = 0;
                for (int i = 0; i < tvShowRespnse.TvShows.Length; i++)
                {
                    try
                    {
                        var castRespnse = await _tvMazeApi.GetCastsAsync(tvShowRespnse.TvShows[i].Id);

                        result.Add(new TvShow()
                        {
                            Id = tvShowRespnse.TvShows[i].Id,
                            Name = tvShowRespnse.TvShows[i].Name,

                            casts = castRespnse.Casts.Select(c => new Cast()
                            {
                                Id = c.Person.Id,
                                Name = c.Person.Name,
                                Birthday = c.Person.Birthday,
                            }).ToArray(),
                            RawResponse = tvShowRespnse.RawResponse
                        });

                        lastSuccedCastCall = i;

                    }
                    catch (Exception ex)
                    {
                        // retry again if an error happened because of the rate limiting
                        _logger.LogError("an error ocurred during call cast api ", ex);
                        i = lastSuccedCastCall + 1;
                    }
                }

                return result.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError("an error ocurred during call TvShow api ", ex);
                throw;
            }
        }
    }
}
