namespace FM_API.Persistance.Repositories
{
    public class BudgetRepository : GenericRepository<Budget>
    {
        public BudgetRepository(DbContext context) : base(context)
        {
        }
    }
}
