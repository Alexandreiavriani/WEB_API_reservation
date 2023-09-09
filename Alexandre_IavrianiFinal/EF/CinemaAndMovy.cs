namespace Alexandre_IavrianiFinal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CinemaAndMovy
    {
        public int Id { get; set; }

        public int CinemaId { get; set; }

        public int MoviesId { get; set; }

        public virtual Cinema Cinema { get; set; }

        public virtual Movy Movy { get; set; }
    }
}
