using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TvMaze.Scraper.Data.Entities
{
    public class TvShowCast
    {
        public int CastId { get; set; }
        public Cast Cast { get; set; }
        public int TvShowId { get; set; }
        public TvShow TvShow { get; set; }

    }
}
