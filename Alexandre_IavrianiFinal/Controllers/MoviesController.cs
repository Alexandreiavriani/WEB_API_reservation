using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Alexandre_IavrianiFinal.EF;

namespace Alexandre_IavrianiFinal.Controllers
{
    public class MoviesController : ApiController
    {
        FinaluriModel db = new FinaluriModel();

       //GET ALL
       [HttpGet]
       [ActionName("GetMovie")]
      
        public IEnumerable<MovieDTO> GetStoreMovie()
        {
            return db.Movies.Select(i => new MovieDTO
            {
                Id_movie=i.id_movie,
                Name=i.name,
                Genre=i.genre,
                Time=i.time,
                MoviePremiere=i.moviePremiere,
                price=i.price
            }).ToList();
        }

        //GET BY ID
        [ResponseType(typeof(MovieDTO))]

        [HttpGet]
        [ActionName("GetMovie")]
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = db.Movies.FirstOrDefault(i => i.id_movie == id);

            if (movie == null) return NotFound(); 

            var result = new MovieDTO
            {
                Id_movie = movie.id_movie,
                Name = movie.name,
                Genre = movie.genre,
                Time = movie.time,
                MoviePremiere = movie.moviePremiere,
                price = movie.price
            };

            return Ok(movie);
        }


        // POST

        [ResponseType(typeof(MovieDTO))]
       
       
        public void Post(MovieDTO movie)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);

            Movie mov = new Movie
            {
                
                name = movie.Name,
                genre = movie.Genre,
                time = movie.Time,
                moviePremiere = movie.MoviePremiere,
                price = movie.price
            };

            db.Movies.Add(mov);
            db.SaveChanges();

            movie.Id_movie = mov.id_movie;
        }



        // PUT
        [ResponseType(typeof(bool))]
        [HttpPut]
        [ActionName("GetMovie")]
        public IHttpActionResult Put(int id, MovieDTO movie)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != movie.Id_movie) return BadRequest();

            try
            {
                Movie mov = db.Movies.FirstOrDefault(i => i.id_movie == id);
                mov.name = movie.Name;
                mov.genre = movie.Genre;
                mov.time = movie.Time;
                mov.moviePremiere = movie.MoviePremiere;
                mov.price = movie.price;
                
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id)) return NotFound();

                else throw;
            }

            return Ok("Movie Updated");
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.id_movie == id) > 0;
        }


        //DELETE

        [HttpDelete]
        [ActionName("GetMovie")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid MOVIE id");

            using (var ctx = new FinaluriModel())
            {
                var movie = ctx.Movies
                    .Where(s => s.id_movie == id)
                    .FirstOrDefault();

                ctx.Entry(movie).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}