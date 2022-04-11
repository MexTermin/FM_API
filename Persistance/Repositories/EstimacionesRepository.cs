namespace FM_API.Persistance.Repositories
{
    public class EstimacionesRepository : GenericRepository<Estimaciones>
    {
        public EstimacionesRepository(DbContext context) : base(context)
        {
        }
    }
}
