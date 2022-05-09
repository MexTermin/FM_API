namespace FMAPI.Persistance.Repositories
{
    public class TransactionIncomeRepository : GenericRepository<TransactionIncome>
    {
        public TransactionIncomeRepository(DbContext _context) : base(_context)
        {
        }
    }
}
