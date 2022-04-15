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
            public virtual DbSet<Estimate> Estimaciones { get; set; }
            public virtual DbSet<Spent> Gastos { get; set; }
            public virtual DbSet<Income> Ingresos { get; set; }
            public virtual DbSet<Budget> Presupuesto { get; set; }
            public virtual DbSet<Rol> Rol { get; set; }
            public virtual DbSet<Transaction> Transacciones { get; set; }
            public virtual DbSet<User> Usuario { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estimate>().ToTable("estimaciones");
            modelBuilder.Entity<Spent>().ToTable("gastos");
            modelBuilder.Entity<Income>().ToTable("ingresos");
            modelBuilder.Entity<Budget>().ToTable("presupuesto");
            modelBuilder.Entity<Rol>().ToTable("rol");
            modelBuilder.Entity<Transaction>().ToTable("transacciones");
            modelBuilder.Entity<User>().ToTable("usuario");
        }
    }
}
