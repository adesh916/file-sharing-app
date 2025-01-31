using Microsoft.EntityFrameworkCore;

namespace FileSharingApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FileModel> Files { get; set; }
    }
}