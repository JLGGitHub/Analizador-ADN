using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ContextDb
{
    public partial  class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base (options)
        {

        }
        public virtual DbSet<Adn> Adns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Adn>(entity =>
            {
                entity.Property(e => e.AdnChain).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    }
}
