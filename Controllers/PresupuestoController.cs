using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PresupuestoController : ControllerBase, IGenericCRUD<PresupuestoDTO>
    {
        protected PresupuestoRepository _repository;
        protected IMapper _mapper;

        public PresupuestoController(PresupuestoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PresupuestoDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Budget>(entity));
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
            IEnumerable<Budget> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<PresupuestoDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Budget result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<PresupuestoDTO>(result));
        }

        [HttpPut]
        public async Task Update(PresupuestoDTO entity)
        {
            await _repository.Update(_mapper.Map<Budget>(entity));
        }
    }
}
