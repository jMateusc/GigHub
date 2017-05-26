using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Following
    {
        /*Lembrando que esta é uma tabela de composição onde
         *apenas relaciona 2 tabelas e associa suas id<->id
         *por isso o nome de tabela de composição
         */

        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }   //Seguidor
        [Key]
        [Column(Order = 2)]
        public string FolloweeId { get; set; }   //Pessoa que é seguida por outra


        //Associa
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}