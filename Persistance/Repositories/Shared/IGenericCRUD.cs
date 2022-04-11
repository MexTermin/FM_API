using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FM_API.Persistance.Repositories.Shared
{
    public interface IGenericCRUD<T>
    {
        public Task<IActionResult> Create(T entity);
        public Task<IActionResult> GetById(long IdEntity);
        public Task<IActionResult> Get();
        public Task Update(T entity);
        public Task Delete(long idEntity);
    }
}
