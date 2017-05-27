using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        /* ViewModel que agrega Tabela Gig e ApplicationUser
         * com a finalidade de checar se usuario est� logado ou n�o
         * a fim de exibir bot�es de a��o ShowActions
         */
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; } 


        //Exibir Cabe�alho Corretamente se usuario estiver logado
        public bool PaginaInicial { get; set; }
    }
}