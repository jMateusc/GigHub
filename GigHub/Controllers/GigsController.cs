﻿using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
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
            throw new System.NotImplementedException();
        }
    }
}