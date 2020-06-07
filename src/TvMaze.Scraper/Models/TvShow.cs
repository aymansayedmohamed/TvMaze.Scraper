using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TvMaze.Scraper.Models
{
    public class TvShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Cast[] Casts { get; set; }
    }
}
