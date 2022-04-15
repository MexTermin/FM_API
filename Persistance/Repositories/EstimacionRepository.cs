namespace FM_API.Persistance.Repositories
{
    public class EstimacionRepository : GenericRepository<Estimate>
    {
        public EstimacionRepository(DbContext context) : base(context)
        {
        }
    }
}
