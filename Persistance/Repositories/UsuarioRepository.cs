namespace FM_API.Persistance.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>
    {
        public UsuarioRepository(DbContext context) : base(context)
        {
        }
    }
}
