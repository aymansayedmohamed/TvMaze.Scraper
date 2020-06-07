using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMaze.Scraper.Responses;

namespace TvMaze.Scraper.Requests
{
    public class TvShowsQuery : IRequest<TvShowResponseModel>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

    }
}
