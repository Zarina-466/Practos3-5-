using Microsoft.EntityFrameworkCore;
using PhotoService.Model;

namespace PhotoService.DatabaseContext
{
    public class TestApiDB : DbContext
    {
        public TestApiDB(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Photos> Photos { get; set; }
    }
}
