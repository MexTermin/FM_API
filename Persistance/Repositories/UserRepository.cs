
namespace FM_API.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task UpdateUser(User entity)
        {
            var atEntity = _dbSet.Attach(entity);
            _DataContext.Entry(entity).State = EntityState.Modified;
            if (entity.Pass == null) atEntity.Property("Pass").IsModified = false;
            if (entity.Id_rol == 0) atEntity.Property("Id_rol").IsModified = false;
            await _DataContext.SaveChangesAsync();
        }
    }
}
