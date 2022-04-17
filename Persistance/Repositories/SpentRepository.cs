namespace FM_API.Persistance.Repositories
{
    public class SpentRepository : GenericRepository<Spent>
    {
        public SpentRepository(DbContext context) : base(context)
        {
        }
    }
}
