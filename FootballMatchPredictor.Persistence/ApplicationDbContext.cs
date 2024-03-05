using FootballMatchPredictor.Domain.Entities;
using FootballMatchPredictor.Persistence.Interceptor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchPredictor.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Bet> Bets { get; set; }
        public DbSet<BetType> BetTypes { get; set; }
        public DbSet<BetValue> BetValues { get; set; }
        public DbSet<BetValueInfo> BetValueInfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Coefficient> Coefficients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
