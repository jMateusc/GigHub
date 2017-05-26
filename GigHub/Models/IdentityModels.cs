using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GigHub.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //ADICIONADO novo campo na tabela AspNetUsers
        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        //A SER ENTENDIDO
        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        //Relaciona Tabelas Criadas ao Contexto
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        //*************************
        //FluentAPI OnDeleteCascade Alterações
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Cada Comparecimento possui uma Gig associada  Many-to-Many
            modelBuilder.Entity<Attendance>().HasRequired(a => a.Gig).WithMany().WillCascadeOnDelete(false);

            //A ENTENDER (um usuario pode possuir varios seguidores) && (um usuario pode seguir varias pessoas)
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Followers).WithRequired(f => f.Followee).WillCascadeOnDelete(false);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Followees).WithRequired(f => f.Follower).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}