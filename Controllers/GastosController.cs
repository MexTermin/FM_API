using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GastosController : ControllerBase, IGenericCRUD<GastosDTO>
    {
        protected GastosRepository _repository;
        protected IMapper _mapper;

        public GastosController(GastosRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GastosDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Gastos>(entity));
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
            IEnumerable<Gastos> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<UsuarioDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Gastos result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<GastosDTO>(result));
        }

        [HttpPut]
        public async Task Update(GastosDTO entity)
        {
            await _repository.Update(_mapper.Map<Gastos>(entity));
        }

    }
}
