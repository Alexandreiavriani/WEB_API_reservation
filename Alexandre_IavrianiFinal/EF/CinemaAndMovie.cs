namespace Alexandre_IavrianiFinal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CinemaAndMovie")]
    public partial class CinemaAndMovie
    {
        [Key]
        public int id_cinemaAndMovie { get; set; }

        public int id_cinema { get; set; }

        public int id_movie { get; set; }

        public virtual Cinema Cinema { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
