using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.Scraper.Infrastructure.Http.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
