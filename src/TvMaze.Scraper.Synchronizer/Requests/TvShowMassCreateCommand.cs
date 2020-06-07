using MediatR;
using System;
using System.Collections.Generic;
using TvMaze.Scraper.Synchronizer.Models;

namespace TvMaze.Scraper.Synchronizer.Requests
{
    public class TvShowMassCreateCommand : IRequest
    {
        public List<TvShow> TvShows { get; set; }

    }
}
