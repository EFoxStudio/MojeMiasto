using Microsoft.EntityFrameworkCore;
using MojeMiasto.Models;

namespace MojeMiasto.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }


    }
}
