using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Artist { get; set; } //Artistas

        public DateTime DateTime { get; set; }      //Data

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }           //Local

        [Required]
        public Genre Genre { get; set; }            //Associa


    }
}