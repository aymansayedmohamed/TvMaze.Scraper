using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TvMaze.Scraper.Infrastructure.Http.Exceptions
{
    public class HttpFailedRequestException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; }

        public HttpFailedRequestException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
