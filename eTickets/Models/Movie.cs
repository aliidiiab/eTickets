using System.ComponentModel.DataAnnotations;
using System;
using eTickets.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using eTickets.Data.Services;
using eTickets.Data.Base;

namespace eTickets.Models
{
    public class Movie: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Descreption { get; set; }

        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public MovieCategory MovieCategory { get; set; }

        public List<Actor_Movie> Actors_Movies { get; set; }
        
        public int CinemaID { get; set; }
        [ForeignKey("CinemaID")]
        public Cinema Cinema   { get; set; }

        public int ProducerID { get; set; }
        [ForeignKey("ProducerID")]
        public Producer Producer { get; set; }


    }
}
