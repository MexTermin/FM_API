namespace FM_API.Persistance.Repositories
{
    public class RolRepository : GenericRepository<Rol>
    {
        public RolRepository(DbContext context) : base(context)
        {
        }
    }
}
