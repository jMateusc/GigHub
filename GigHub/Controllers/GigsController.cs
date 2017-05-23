using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {
            var viewmodel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            return View(viewmodel);
        }

        /*Oq essa ação deve executar e oq ela precisa ser passada como parâmetros?
         *1-Apenas Usuario Registrado deve possuir acesso ao formulario
         *2-Ao submeter formulário deve ser relacionado ao usuario(Id) que a submeteu
         *  provindo da tabela ApplicationUser<AspNetUsers>(usuario sao cadastrados nela)
         *3-2 Tabelas são populadas: Gigs e Genre, para tal deve ser criado uma ViewModel
         *  afim de ter acesso as propriedades(campos) das 2 Model na view exibida(Create)
         *4-Após submissão, deve ser redirecionado a página inicial
         */
        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //MODO LENTO
            //var artistId = User.Identity.GetUserId();                          
            //var artist = _context.Users.Single(u => u.Id == artistId);         //existe mesmo esse usuario?
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);  //existe mesmo esse genero?

            //Validação
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList(); //para formulario não submeter vazio
                return View("Create", viewModel);
            }


            var gig = new Gig
            {
                //resgata usuario.id logado
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}