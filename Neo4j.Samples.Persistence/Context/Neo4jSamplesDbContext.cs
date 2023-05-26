using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Neo4j.Samples.Application;
using Neo4j.Samples.Domain.Entities;

namespace Neo4j.Samples.Persistence.Context
{
    public class Neo4jSamplesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public Neo4jSamplesDbContext() : base()
        {

        }

        public Neo4jSamplesDbContext(DbContextOptions<Neo4jSamplesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                                new User
                                (   1,
                                    "a@a.com",
                                    "123"
                                ),
                                new User
                                (
                                    2,
                                    "b@b.com",
                                    "123"
                                )
                         );

            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                  var currentDirectory = Directory.GetCurrentDirectory();
                  string parentDirectory = Path.GetDirectoryName(currentDirectory);

                  IConfiguration configuration = new ConfigurationBuilder()
                                   .SetBasePath(parentDirectory + "/Neo4j.Samples.API")
                                   .AddJsonFile("appsettings.json")
                                   .Build();

                string connectionString = configuration.GetConnectionString("OnionArchitectureDB");

                optionsBuilder
                   .UseSqlServer(connectionString)
                   .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
            }

        }
    }
}

