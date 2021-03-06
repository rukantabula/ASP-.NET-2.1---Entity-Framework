﻿using System;
using System.Collections.Generic;
using System.Text;
using SamuraiApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
       
        public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options) { }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }


        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = SamuraiAppData: Trusted_Connection = True;");
        } */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Joining tow tables to get many to many relationship - Fluent api to mapping
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.SamuraiId, s.BattleId });
        }
    }
}
