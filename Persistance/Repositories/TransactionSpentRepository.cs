namespace FMAPI.Persistance.Repositories
{
    public class TransactionSpentRepository : GenericRepository<TransactionSpent>
    {
        public TransactionSpentRepository(DbContext _context) : base(_context)
        {
        }
    }
}
