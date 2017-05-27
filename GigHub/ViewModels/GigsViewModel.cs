using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        /* ViewModel que agrega Tabela Gig e ApplicationUser
         * com a finalidade de checar se usuario está logado ou não
         * a fim de exibir botões de ação ShowActions
         */
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; } 


        //Exibir Cabeçalho Corretamente se usuario estiver logado
        public bool PaginaInicial { get; set; }
    }
}