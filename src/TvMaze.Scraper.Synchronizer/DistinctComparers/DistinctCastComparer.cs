using System;
using System.Collections.Generic;
using System.Text;
using TvMaze.Scraper.Data.Entities;

namespace TvMaze.Scraper.Synchronizer.DistinctComparers
{
    class DistinctCastComparer : IEqualityComparer<Cast>
    {

        public bool Equals(Cast x, Cast y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Cast obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
