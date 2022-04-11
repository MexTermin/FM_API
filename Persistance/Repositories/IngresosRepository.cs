namespace FM_API.Persistance.Repositories
{
    public class IngresosRepository : GenericRepository<Ingresos>
    {
        public IngresosRepository(DbContext context) : base(context)
        {
        }
    }
}
