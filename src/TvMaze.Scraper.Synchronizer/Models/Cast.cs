using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TvMaze.Scraper.Synchronizer.Models
{
    public class Cast
    {
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string RawResponse { get; set; }


    }
}
