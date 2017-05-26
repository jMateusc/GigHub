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
        public ActionResult Index()
        {
            //extrai o Artista(tabela) relacionado a Gig(tabela) que irá participar.
            //Evitando assim NullReferenceException
            var upcomingGigs = _context.Gigs
                                       .Include(g => g.Artist)
                                       .Include(g => g.Genre)
                                       .Where(g => g.DateTime > DateTime.Now);

            //HomeViewModel para checar se Usuario está Logado ou Não (caso nao esteja, não exibe botões Follow e Going)
            var viewModel = new HomeViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated
            };
            return View(viewModel);
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