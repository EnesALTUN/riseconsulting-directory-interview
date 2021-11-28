using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RiseConsulting.Directory.Entities.Models;

namespace RiseConsulting.Directory.Data
{
    public class RiseConsultingDirectoryDbContext : DbContext
    {
        public RiseConsultingDirectoryDbContext()
        {

        }

        public RiseConsultingDirectoryDbContext(DbContextOptions<RiseConsultingDirectoryDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public virtual DbSet<CommunicationInformation> CommunicationInformation { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<DirectoryUsers> DirectoryUsers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}