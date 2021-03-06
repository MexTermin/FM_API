
#nullable disable

using System.Linq;
using System.Linq.Expressions;
using Type = FMAPI.Entities.Type;

namespace FM_API.Persistance.Database
{
    public class FMContext : DbContext
    {
        public FMContext(DbContextOptions<FMContext> options) : base(options) { }

        #region dbSets
        public virtual DbSet<Estimate> Estimaciones { get; set; }
        public virtual DbSet<Budget> Presupuesto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Transaction> Transacciones { get; set; }
        public virtual DbSet<User> Usuario { get; set; }
        public virtual DbSet<Category> Categoria { get; set; }
        public virtual DbSet<Type> Tyype { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estimate>().ToTable("estimaciones");
            modelBuilder.Entity<Budget>().HasQueryFilter(ent => EF.Property<DateTime?>(ent, "Deleted_at") == null).ToTable("presupuesto");
            modelBuilder.Entity<Rol>().ToTable("rol");
            modelBuilder.Entity<Transaction>().ToTable("transacciones");
            modelBuilder.Entity<User>().ToTable("usuario");
            modelBuilder.Entity<Type>().ToTable("type");
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
