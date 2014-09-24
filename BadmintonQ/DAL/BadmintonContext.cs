using BadmintonQ.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BadmintonQ.DAL
{
    public class BadmintonContext: DbContext
    {
        public BadmintonContext() : base("BadmintonContext"){}      
        
        public DbSet<PlayerModels> Players { get; set; }
        public DbSet<ActivePlayer> ActivePlayers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivePlayer>().ToTable("ActivePlayers");
            modelBuilder.Entity<PlayerModels>().ToTable("Players");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}