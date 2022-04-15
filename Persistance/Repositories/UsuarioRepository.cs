namespace FM_API.Persistance.Repositories
{
    public class UsuarioRepository : GenericRepository<User>
    {
        public UsuarioRepository(DbContext context) : base(context)
        {
        }
    }
}
