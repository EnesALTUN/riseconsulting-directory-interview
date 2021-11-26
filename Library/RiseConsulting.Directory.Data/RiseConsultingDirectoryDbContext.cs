using Microsoft.EntityFrameworkCore;
using RiseConsulting.Directory.Entities.Models;

namespace RiseConsulting.Directory.Data
{
    public class RiseConsultingDirectoryDbContext : DbContext
    {
        public RiseConsultingDirectoryDbContext(DbContextOptions<RiseConsultingDirectoryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommunicationInformation> CommunicationInformation { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<DirectoryUsers> DirectoryUsers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}