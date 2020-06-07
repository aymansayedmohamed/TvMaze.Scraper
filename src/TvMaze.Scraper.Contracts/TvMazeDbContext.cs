using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TvMaze.Scraper.Data.Entities;

namespace TvMaze.Scraper.Data
{
    public class TvMazeDbContext : DbContext
    {
        public TvMazeDbContext()
        {
        }

        public TvMazeDbContext(DbContextOptions<TvMazeDbContext> options) : base(options)
        {

        }
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Cast> Casts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TvShowCast>()
                .HasKey(o => new { o.TvShowId, o.CastId });
            modelBuilder.Entity<TvShowCast>()
                .HasOne(bc => bc.TvShow)
                .WithMany(b => b.TvShowCasts)
                .HasForeignKey(bc => bc.TvShowId);
            modelBuilder.Entity<TvShowCast>()
                .HasOne(bc => bc.Cast)
                .WithMany(c => c.TvShowCasts)
                .HasForeignKey(bc => bc.CastId);

        }
    }
}
