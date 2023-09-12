using Microsoft.EntityFrameworkCore;
using Wedding.Shared.Models;

namespace Wedding.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Account> Accounts { get; set; }
    }
}