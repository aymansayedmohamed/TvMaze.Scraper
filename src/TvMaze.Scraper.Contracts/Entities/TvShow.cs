using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TvMaze.Scraper.Data.Entities
{
    public class TvShow
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string RawResponse { get; set; }
        public ICollection<TvShowCast> TvShowCasts { get; set; }
    }
}
