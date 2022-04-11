using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolController : ControllerBase, IGenericCRUD<RolDTO>
    {
        protected RolRepository _repository;
        protected IMapper _mapper;

        public RolController(RolRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Rol>(entity));
            return Ok(result);
        }
        [HttpDelete]
        public async Task Delete(long idEntity)
        {
            await _repository.Delete(idEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Rol> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<RolDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Rol result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<RolDTO>(result));
        }

        [HttpPut]
        public async Task Update(RolDTO entity)
        {
            await _repository.Update(_mapper.Map<Rol>(entity));
        }
    }
}
