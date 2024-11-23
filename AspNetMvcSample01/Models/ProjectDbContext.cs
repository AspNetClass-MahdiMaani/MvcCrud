using AspNetMvcSample01.Models.DomainModels.PersonAggregates;
using Microsoft.EntityFrameworkCore;


namespace AspNetMvcSample01.Models
{
    public class ProjectDbContext : DbContext
    {
            public ProjectDbContext(DbContextOptions options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }

            public DbSet<Person> Person { get; set; }



       
    }
}
