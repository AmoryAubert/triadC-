using TriadServer.Contexts.Configs;
using TriadServer.Models;
using Microsoft.EntityFrameworkCore;

namespace TriadServer.Contexts
{
    public class TriadContext : DbContext
    {
        public DbSet<User> Users
        {
            get
            {
                return Set<User>();
            }
        }
        public DbSet<Card> Cards
        {
            get
            {
                return Set<Card>();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Triad;Integrated Security=True;");        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new CardConfig());
        }
    }
}
