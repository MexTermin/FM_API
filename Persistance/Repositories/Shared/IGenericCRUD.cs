using Microsoft.AspNetCore.Mvc;

namespace FM_API.Persistance.Repositories.Shared
{
    public interface IGenericCRUD<T>
    {
        public Task<IActionResult> Create(T entity);
        public Task<IActionResult> GetById(long IdEntity);
        public Task<IActionResult> Get();
        public Task<IActionResult> Update(T entity);
        public Task<IActionResult> Delete(long idEntity);
    }
}
