namespace FMAPI.Persistance.Repositories
{
    public class Estimate_IncomeRepository : GenericRepository<Estimate_Income>
    {
        public Estimate_IncomeRepository(DbContext _context) : base(_context)
        {
        }
    }
}
