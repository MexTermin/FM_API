namespace FM_API.Persistance.Repositories
{
    public class PresupuestoRepository : GenericRepository<Presupuesto>
    {
        public PresupuestoRepository(DbContext context) : base(context)
        {
        }
    }
}
