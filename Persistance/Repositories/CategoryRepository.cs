
namespace FMAPI.Persistance.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(DbContext _context) : base(_context)
        {
        }
    }
}
