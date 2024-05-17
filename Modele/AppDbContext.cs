using Microsoft.EntityFrameworkCore;
using Modele;

namespace WymianaWaluty.Modele
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}