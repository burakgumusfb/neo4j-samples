using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Mini.Social.Media.Domain.Entities;

namespace Mini.Social.Media.Persistence.Context
{
    public class MiniSocialMediaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public MiniSocialMediaDbContext() : base()
        {

        }

        public MiniSocialMediaDbContext(DbContextOptions<MiniSocialMediaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                string parentDirectory = Path.GetDirectoryName(currentDirectory);

                IConfiguration configuration = new ConfigurationBuilder()
                                 .SetBasePath(parentDirectory + "/Mini.Social.Media.API")
                                 .AddJsonFile("appsettings.json")
                                 .Build();

                string connectionString = configuration.GetConnectionString("Neo4jSamplesDB");

                optionsBuilder
                   .UseSqlServer(connectionString)
                   .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
            }

        }
    }
}

