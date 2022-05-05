namespace FMAPI.Persistance.Repositories
{
    public class EstimateSpentRepository : GenericRepository<EstimateSpent>
    {
        public EstimateSpentRepository(DbContext _context) : base(_context)
        {
        }
    }
}
