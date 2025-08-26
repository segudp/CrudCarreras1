using CrudCarreras1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CrudCarreras1.Datos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Instituto> Institutos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Carrera>()
                .HasOne(c => c.Instituto)
                .WithMany(i => i.Carreras)
                .HasForeignKey(c => c.InstitutoId)
                .OnDelete(DeleteBehavior.Cascade); // o Restrict, según lo que prefieras
        }


    }

}
