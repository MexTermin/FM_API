namespace FM_API.Persistance.Repositories
{
    public class EstimateRepository : GenericRepository<Estimate>
    {
        public EstimateRepository(DbContext context) : base(context)
        {
        }
    }
}
