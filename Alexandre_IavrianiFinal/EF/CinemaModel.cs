using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Alexandre_IavrianiFinal.EF
{
    public partial class CinemaModel : DbContext
    {
        public CinemaModel()
            : base("name=CinemaModel")
        {
        }

        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<CinemaAndMovy> CinemaAndMovies { get; set; }
        public virtual DbSet<Movy> Movies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Cinema>()
                .Property(e => e.Address)
                .IsFixedLength();

            modelBuilder.Entity<Cinema>()
                .HasMany(e => e.CinemaAndMovies)
                .WithRequired(e => e.Cinema)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movy>()
                .Property(e => e.Genre)
                .IsFixedLength();

            modelBuilder.Entity<Movy>()
                .Property(e => e.Time)
                .IsFixedLength();

            modelBuilder.Entity<Movy>()
                .HasMany(e => e.CinemaAndMovies)
                .WithRequired(e => e.Movy)
                .HasForeignKey(e => e.MoviesId)
                .WillCascadeOnDelete(false);
        }
    }
}
