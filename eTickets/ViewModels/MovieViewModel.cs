using eTickets.Data;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.ViewModels
{
    public class MovieViewModel
    {
        [Required]
        [Display(Name ="Movie Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Movie Descreption")]
        public string Descreption { get; set; }
        [Required]
        [Display(Name = "Movie Price in $")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Movie Poster URL")]
        public string ImageURL { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Select a Category")]
        public MovieCategory MovieCategory { get; set; }
        [Required]
        [Display(Name = "Select Movie Actor(s)")]
        public List<int> ActorIds { get; set; }
        [Required]
        [Display(Name = "Select a Cinema")]
        public int CinemaID { get; set; }
        [Required]
        [Display(Name = "Select a Producer")]
        public int ProducerID { get; set; }
    }
}
