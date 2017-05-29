using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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
        // Formulário
        [Authorize]
        public ActionResult GigForm()
        {
            var viewmodel = new GigFormViewModel
            {
                Cabeçalho = "Add a Gig",
                Genres = _context.Genres.ToList()
            };
            return View(viewmodel);
        }

        // Editar um show 
        [Authorize]
        public ActionResult Edit(int id)
        {
            var logado = User.Identity.GetUserId();
            var lista = _context.Gigs.Single(g => g.Id == id && logado == g.ArtistId);
            var viewmodel = new GigFormViewModel
            {
                Cabeçalho = "Editar",
                Genres = _context.Genres.ToList(),
                Venue = lista.Venue,
                Date = lista.DateTime.ToString("d MMM yyyy"),
                Time = lista.DateTime.ToString("HH:mm"),
                Genre = lista.GenreId
            };
            return View("GigForm",viewmodel);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //MODO LENTO
            //var artistId = User.Identity.GetUserId();                          
            //var artist = _context.Users.Single(u => u.Id == artistId);         //existe mesmo esse usuario?
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);  //existe mesmo esse genero? Select genre From Genre Where (id==id)

            //Validação
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList(); //para formulario não submeter vazio
                return View("GigForm", viewModel);
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

        /*Ação responsável por identificar e retornar a View informações relacionado ao usuario que irá
         *participar de algum dos eventos que o mesmo demonstrou interesse ao clicar (going?) */
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();

            /*assim como na action Index() do Controller HomeController
             *Acumula informações relacionado a shows que o usuario logado irá comparecer
             *informações essas que são: Gig,Artista,Genero
             *podendo exibir asism, datas do show, nome do artista a tocar, genero musical além de outras informações */
            var gigsViewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                
            };

            return View("Attending",gigsViewModel);  //
        }

        /* Ação responsável por identificar quem o usuário que está logado no momento está seguindo.*/
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var artistasQueSigo = _context.Followings
                                 .Where(f => f.FollowerId == userId)
                                 .Select(f => f.Followee)
                                 .ToList();

            

            return View("Following", artistasQueSigo);
            
        }

        /* Ação responsavel por exibir quais artistas/shows determinado usuario logado irá comparecer */
        [Authorize]
        public ActionResult MineUpcomingGigs()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                               .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                               .Include(g => g.Genre)
                               .ToList();
            /*Query em LINQ <-> SQL */
            var retorna_atributos_de_gigs = from s in _context.Gigs
                                            join a in _context.Genres  
                                            on s.GenreId equals a.Id 
                                            where s.ArtistId == userId
                                            select s;

            return View(retorna_atributos_de_gigs);
        }
    }
}