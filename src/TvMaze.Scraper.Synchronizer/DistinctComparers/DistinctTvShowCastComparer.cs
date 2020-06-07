using System;
using System.Collections.Generic;
using System.Text;
using TvMaze.Scraper.Data.Entities;

namespace TvMaze.Scraper.Synchronizer.DistinctComparers
{
    class DistinctTvShowCastComparer : IEqualityComparer<TvShowCast>
    {

        public bool Equals(TvShowCast x, TvShowCast y)
        {
            return x.CastId == y.CastId
                && x.TvShowId == y.TvShowId;
        }

        public int GetHashCode(TvShowCast obj)
        {
            return obj.CastId.GetHashCode() ^ obj.TvShowId.GetHashCode();
        }
    }
}
