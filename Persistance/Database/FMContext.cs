using FM_API.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FM_API.Persistance.Database
{
    public class FMContext : DbContext
    {
        public FMContext(DbContextOptions<FMContext> options) : base(options)
        { }
            #region dbSets
            public virtual DbSet<Estimaciones> Estimaciones { get; set; }
            public virtual DbSet<Gastos> Gastos { get; set; }
            public virtual DbSet<Ingresos> Ingresos { get; set; }
            public virtual DbSet<Presupuesto> Presupuesto { get; set; }
            public virtual DbSet<Rol> Rol { get; set; }
            public virtual DbSet<Transacciones> Transacciones { get; set; }
            public virtual DbSet<Usuario> Usuario { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estimaciones>().ToTable("estimaciones");
            modelBuilder.Entity<Gastos>().ToTable("gastos");
            modelBuilder.Entity<Ingresos>().ToTable("ingresos");
            modelBuilder.Entity<Presupuesto>().ToTable("presupuesto");
            modelBuilder.Entity<Rol>().ToTable("rol");
            modelBuilder.Entity<Transacciones>().ToTable("transacciones");
            modelBuilder.Entity<Usuario>().ToTable("usuario");
        }
    }
}
