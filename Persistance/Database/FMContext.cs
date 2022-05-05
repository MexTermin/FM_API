
#nullable disable

using System.Linq;
using System.Linq.Expressions;

namespace FM_API.Persistance.Database
{
    public class FMContext : DbContext
    {
        public FMContext(DbContextOptions<FMContext> options) : base(options) { }

        #region dbSets
        public virtual DbSet<Estimate> Estimaciones { get; set; }
        public virtual DbSet<EstimateIncome> Estimate_Income { get; set; }
        public virtual DbSet<EstimateSpent> Estimate_Spent { get; set; }

        public virtual DbSet<Spent> Gastos { get; set; }
        public virtual DbSet<Income> Ingresos { get; set; }
        public virtual DbSet<Budget> Presupuesto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Transaction> Transacciones { get; set; }
        public virtual DbSet<User> Usuario { get; set; }
        public virtual DbSet<Category> Categoria { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estimate>().ToTable("estimaciones");
            modelBuilder.Entity<EstimateIncome>().ToTable("estimate_income");
            modelBuilder.Entity<EstimateSpent>().ToTable("estimate_spent");

            modelBuilder.Entity<Spent>().ToTable("gastos");
            modelBuilder.Entity<Income>().ToTable("ingresos");
            modelBuilder.Entity<Budget>().HasQueryFilter(ent => EF.Property<DateTime?>(ent, "Deleted_at") == null).ToTable("presupuesto");
            modelBuilder.Entity<Rol>().ToTable("rol");
            modelBuilder.Entity<Transaction>().ToTable("transacciones");
            modelBuilder.Entity<User>().ToTable("usuario");
            modelBuilder.Entity<Category>().HasQueryFilter(ent => EF.Property<DateTime?>(ent, "Deleted_at") == null).ToTable("categoria");
        }
        public override int SaveChanges()
        {

            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                DateTime? Date;
                bool isSoftDelete = entry.CurrentValues.TryGetValue("Deleted_at", out Date);
                if (isSoftDelete)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["Deleted_at"] = null;
                            entry.CurrentValues["Update_at"] = DateTime.UtcNow;
                            entry.CurrentValues["Create_at"] = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["Deleted_at"] = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues["Update_at"] = DateTime.UtcNow;
                            break;
                    }
                }
            }
        }
    }
}
