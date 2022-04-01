using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Los_Portales.Models;

namespace Los_Portales.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Los_Portales.Models.Admin> Admin { get; set; }
        public DbSet<Los_Portales.Models.Play> Play { get; set; }
        public DbSet<Los_Portales.Models.Seat> Seat { get; set; }
       
        
       
        
        
    }
}