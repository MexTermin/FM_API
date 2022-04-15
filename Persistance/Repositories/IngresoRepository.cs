namespace FM_API.Persistance.Repositories
{
    public class IngresoRepository : GenericRepository<Income>
    {
        public IngresoRepository(DbContext context) : base(context)
        {
        }
    }
}
