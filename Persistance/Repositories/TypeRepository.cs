using Type = FMAPI.Entities.Type;

namespace FMAPI.Persistance.Repositories
{
    public class TypeRepository : GenericRepository<Type>
    {
        public TypeRepository(DbContext context) : base(context)
        {
        }
    }
}
