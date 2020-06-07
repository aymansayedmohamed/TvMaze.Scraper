using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TvMaze.Scraper.Requests;

namespace TvMaze.Scraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvMazeController : ControllerBase
    {
        private readonly IMediator _mediator;


        public TvMazeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<Models.TvShow[]>> Get(int pageNumber, int pageSize)

        {

            var tvShowsQuery = new TvShowsQuery() { pageNumber = pageNumber, pageSize = pageSize };

            var result = await _mediator.Send(tvShowsQuery);
            return result.TvShows;
        }

    }
}
