using Microsoft.EntityFrameworkCore;

using SPG.Model;


namespace SPG.Data.EF
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserMap(modelBuilder.Entity<User>());
            
        }
        public DbSet<User> User { get; set; }
    }
}
