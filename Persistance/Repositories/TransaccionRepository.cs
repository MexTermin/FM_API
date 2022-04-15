namespace FM_API.Persistance.Repositories
{
    public class TransaccionRepository : GenericRepository<Transaction>
    {
        public TransaccionRepository(DbContext context) : base(context)
        {
        }
    }
}
