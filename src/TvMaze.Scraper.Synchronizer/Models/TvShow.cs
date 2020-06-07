using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TvMaze.Scraper.Synchronizer.Models
{
    public class TvShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RawResponse { get; set; }
        public IEnumerable<Cast> Casts { get; set; }
    }
}
