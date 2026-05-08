using Microsoft.EntityFrameworkCore;
using Intprog_FinalPrjoect.Models;
using Intprog_FinalPrjoect.Models.ViewModels;
namespace Intprog_FinalPrjoect.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
    