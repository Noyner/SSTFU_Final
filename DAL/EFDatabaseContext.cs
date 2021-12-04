using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using DTO;
namespace DAO
{
    public class EFDatabaseContext : DbContext
    {
        public EFDatabaseContext()
        {

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=sstfu2021;Trusted_Connection=True;");
        }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)/**//**/
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ECamera> Cameras { get; set; }
        public DbSet<EDistrict> Districts { get; set; } 
        
        public DbSet<EIncidentType> IncidentTypes { get; set; }
        public DbSet<EIncident> Incidents { get; set; }
        public DbSet<EGarbageStatIncident> GarbageStatIncidents { get; set; }

    }

}

