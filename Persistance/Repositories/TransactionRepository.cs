namespace FM_API.Persistance.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }
    }
}
