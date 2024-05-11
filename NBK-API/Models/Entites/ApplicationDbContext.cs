using Microsoft.EntityFrameworkCore;
using NBK_API.Models.Entites.Users;

namespace NBK_API.Models.Entites
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

    }
}