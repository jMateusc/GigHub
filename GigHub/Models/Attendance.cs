using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        //Tabela de Composição User <-> Gig
        //Comparecimento

        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }


        //(FK)
        [Key]
        [Column(Order = 1)]
        public int GigId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}