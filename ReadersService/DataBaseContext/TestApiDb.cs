using Praktos.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Praktos.DataBaseContext
{
    public class TestApiDb : DbContext
    {
        public TestApiDb(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Readers> Readers { get; set; }
    }
}
