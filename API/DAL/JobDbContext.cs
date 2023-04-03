using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
