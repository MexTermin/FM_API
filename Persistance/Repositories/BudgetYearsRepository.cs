namespace FMAPI.Persistance.Repositories
{
    public class BudgetYearsRepository : GenericRepository<BudgetYears>
    {
        public BudgetYearsRepository(DbContext _context) : base(_context)
        {
        }
    }
}
