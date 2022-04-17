namespace FM_API.Persistance.Repositories
{
    public class IncomeRepository : GenericRepository<Income>
    {
        public IncomeRepository(DbContext context) : base(context)
        {
        }
    }
}
