using Microsoft.EntityFrameworkCore;

namespace RetrospectiveApi.Entities
{
    public class ProjectDBContext:DbContext
    {
        public ProjectDBContext(DbContextOptions<ProjectDBContext> options):base(options)
        {
            //Database.Migrate();
            if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                Database.Migrate();
            }


        }

        public DbSet<RetropsectiveProject> RetropsectiveProjects { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
