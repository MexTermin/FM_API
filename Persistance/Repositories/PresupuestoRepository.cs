namespace FM_API.Persistance.Repositories
{
    public class PresupuestoRepository : GenericRepository<Budget>
    {
        public PresupuestoRepository(DbContext context) : base(context)
        {
        }
    }
}
