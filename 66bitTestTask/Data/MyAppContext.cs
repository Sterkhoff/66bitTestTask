using _66bitTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace _66bitTestTask.Data
{
    public class MyAppContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<SoccerPlayer> SoccerPlayers { get; set; }
        public DbSet<Team> Teams { get; set; }

        public MyAppContext(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DbConnectionString"));
        }
    }
}
