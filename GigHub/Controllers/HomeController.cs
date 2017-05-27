using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        /*Esta ação é exiba Home/Index.html*/
        public ActionResult Index()
        {
            //extrai o Artista(tabela) relacionado a Gig(tabela) que irá participar.
            //Evitando assim NullReferenceException
            var upcomingGigs = _context.Gigs
                                       .Include(g => g.Artist)
                                       .Include(g => g.Genre)
                                       .Where(g => g.DateTime > DateTime.Now);

            //GigsViewModel para checar se Usuario está Logado ou Não (caso nao esteja, não exibe botões Follow e Going)
            /*Guarda em memoria todas informações relacionadas a shows que irão acontecer (UpcomingGigs)
             *Além disso verifica se quem está visualizando a página está logado, caso esteja alguns botões(follow e going)
             *são exibidos na tela. */
            var gigsViewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                PaginaInicial = true
            };
            return View("GigsIndex",gigsViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}