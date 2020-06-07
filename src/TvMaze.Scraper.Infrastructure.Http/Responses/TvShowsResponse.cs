using System;
using System.Collections.Generic;
using System.Text;
using TvMaze.Scraper.Infrastructure.Http.Models;

namespace TvMaze.Scraper.Infrastructure.Http.Responses
{
    public class TvShowsResponse
    {
        public string RawResponse { get; set; }
        public TvShow[] TvShows { get; set; }
    }
}
