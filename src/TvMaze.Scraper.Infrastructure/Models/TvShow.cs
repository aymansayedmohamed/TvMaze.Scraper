using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.Scraper.Infrastructure.Models
{
    public class TvShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Cast> casts { get; set; }
        public string RawResponse { get; set; }
    }
}
