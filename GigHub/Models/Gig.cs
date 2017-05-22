using System;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }
        public ApplicationUser Artist { get; set; } //Artistas
        public DateTime DateTime { get; set; }      //Data
        public string Venue { get; set; }           //Local
        public Genre Genre { get; set; }            //Associa


    }
}