using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }
     
        public DateTime DateTime { get; set; }      //Data

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }           //Local

        


        /*2 Models associado a este Model Gig
         */    
        public ApplicationUser Artist { get; set; } //Artistas       
        public Genre Genre { get; set; }            //Genero musical


        [Required]
        public string ArtistId { get; set; }        //fk(id) artista
        [Required]
        public byte GenreId { get; set; }            //fk(id) genero

    }
}