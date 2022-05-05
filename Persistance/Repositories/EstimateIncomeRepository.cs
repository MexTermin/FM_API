namespace FMAPI.Persistance.Repositories
{
    public class EstimateIncomeRepository : GenericRepository<EstimateIncome>
    {
        public EstimateIncomeRepository(DbContext _context) : base(_context)
        {
        }
    }
}
