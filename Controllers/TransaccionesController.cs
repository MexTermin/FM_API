using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransaccionesController : ControllerBase, IGenericCRUD<TransaccionesDTO>
    {
        protected TransaccionesRepository _repository;
        protected IMapper _mapper;

        public TransaccionesController(TransaccionesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransaccionesDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Transacciones>(entity));
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
            IEnumerable<Transacciones> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<TransaccionesDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Transacciones result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<TransaccionesDTO>(result));
        }

        [HttpPut]
        public async Task Update(TransaccionesDTO entity)
        {
            await _repository.Update(_mapper.Map<Transacciones>(entity));
        }
    }
}
