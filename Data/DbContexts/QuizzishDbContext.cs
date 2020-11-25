using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Data.DbContexts
{
    public class QuizzishDbContext : IdentityDbContext
    {
        public QuizzishDbContext(DbContextOptions<QuizzishDbContext> options) : base(options)
        {
            
        }

        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<PlayerSection> PlayerSections { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }

    }
}
