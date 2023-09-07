using Microsoft.EntityFrameworkCore;

namespace Pickleball_project.Models
{
    public class PickleballDBContext : DbContext
    {
        public PickleballDBContext(DbContextOptions<PickleballDBContext>options) : base(options)
        {
            
        }
        public DbSet<Client> Clients { get; set; }
    }
}
