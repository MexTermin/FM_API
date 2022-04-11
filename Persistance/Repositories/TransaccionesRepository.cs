namespace FM_API.Persistance.Repositories
{
    public class TransaccionesRepository : GenericRepository<Transacciones>
    {
        public TransaccionesRepository(DbContext context) : base(context)
        {
        }
    }
}
