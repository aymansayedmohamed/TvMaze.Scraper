﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.Scraper.Infrastructure.Http.Core
{
    public class HttpClientBaseResponse<TResult>
    {
        public HttpClientBaseResponse(string raw)
        {
            Result = JsonConvert.DeserializeObject<TResult>(raw);
            Raw = raw;
        }

        public TResult Result { get; }

        /// <summary>
        /// Raw response received from the rest 
        /// api endpoint.
        /// </summary>
        public string Raw
        {
            get;
        }
    }
}
