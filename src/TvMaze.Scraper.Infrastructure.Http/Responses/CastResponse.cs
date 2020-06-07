using System;
using System.Collections.Generic;
using System.Text;
using TvMaze.Scraper.Infrastructure.Http.Models;

namespace TvMaze.Scraper.Infrastructure.Http.Responses
{
    public class CastResponse
    {
        public string RawResponse { get; set; }
        public IEnumerable<Cast> Casts { get; set; }
    }
}
