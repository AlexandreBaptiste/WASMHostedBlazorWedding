using Microsoft.EntityFrameworkCore;
using Wedding.Shared.Models;

namespace Wedding.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Guest> guests { get; set; }
    }
}
