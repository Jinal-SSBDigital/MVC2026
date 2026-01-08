using Microsoft.EntityFrameworkCore;
using MVC2026.Models;

namespace MVC2026.Data
{
    public class AppDbContext :DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
