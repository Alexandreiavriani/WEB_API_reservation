using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Alexandre_IavrianiFinal.EF
{
    public partial class FinaluriModel : DbContext
    {
        public FinaluriModel()
            : base("name=FinaluriModel")
        {
        }

        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<CinemaAndMovie> CinemaAndMovies { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>()
                .HasMany(e => e.CinemaAndMovies)
                .WithRequired(e => e.Cinema)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.CinemaAndMovies)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);
        }
    }
}
