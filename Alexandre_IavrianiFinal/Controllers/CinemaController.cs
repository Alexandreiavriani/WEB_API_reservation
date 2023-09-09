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
    public class CinemaController : ApiController
    {
        FinaluriModel db = new FinaluriModel();


        //GET ALL
        [HttpGet]
        [ActionName("GetCinema")]

        public IEnumerable<CinemaDTO> GetStoreCinema()
        {
            return db.Cinemas.Select(i => new CinemaDTO
            {
                Id_cinema=i.id_cinema,  
                Name=i.name,
                Adress=i.adress,
            
            }).ToList();
        }

        //GET BY ID
        [ResponseType(typeof(CinemaDTO))]

        [HttpGet]
        [ActionName("GetCinema")]
        public IHttpActionResult GetCinema(int id)
        {
            Cinema cinema = db.Cinemas.FirstOrDefault(i => i.id_cinema == id);

            if (cinema== null) return NotFound();

            var result = new CinemaDTO
            {
               
                Id_cinema = cinema.id_cinema,
                Name = cinema.name,
                Adress = cinema.adress
            };

            return Ok(cinema);
        }


        // POST

        [ResponseType(typeof(CinemaDTO))]


        public void Post(CinemaDTO cinema)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);

            Cinema cin = new Cinema
            {

                name = cinema.Name,
                adress = cinema.Adress,
             
            };

            db.Cinemas.Add(cin);
            db.SaveChanges();

            cinema.Id_cinema = cin.id_cinema;
        }



        // PUT
        [ResponseType(typeof(bool))]
        [HttpPut]
        [ActionName("GetCinema")]
        public IHttpActionResult Put(int id, CinemaDTO cinema)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != cinema.Id_cinema) return BadRequest();

            try
            {
                Cinema cin = db.Cinemas.FirstOrDefault(i => i.id_cinema == id);
                cin.name = cinema.Name;
                cin.adress = cinema.Adress;

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(id)) return NotFound();

                else throw;
            }

            return Ok("Cinema Updated");
        }

        private bool CinemaExists(int id)
        {
            return db.Cinemas.Count(e => e.id_cinema == id) > 0;
        }


        //DELETE

        [HttpDelete]
        [ActionName("GetCinema")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Cinema id");

            using (var ctx = new FinaluriModel())
            {
                var cinema = ctx.Cinemas
                    .Where(s => s.id_cinema == id)
                    .FirstOrDefault();

                ctx.Entry(cinema).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
