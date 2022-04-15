namespace FM_API.Persistance.Repositories
{
    public class GastoRepository : GenericRepository<Spent>
    {
        public GastoRepository(DbContext context) : base(context)
        {
        }
    }
}
