namespace FMAPI.Persistance.Repositories
{
    public class Estimate_SpentRepository : GenericRepository<Estimate_Spent>
    {
        public Estimate_SpentRepository(DbContext _context) : base(_context)
        {
        }
    }
}
