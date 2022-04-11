namespace FM_API.Persistance.Repositories
{
    public class GastosRepository : GenericRepository<Gastos>
    {
        public GastosRepository(DbContext context) : base(context)
        {
        }
    }
}
