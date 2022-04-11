using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstimacionesController : ControllerBase, IGenericCRUD<EstimacionesDTO>
    {
        protected EstimacionesRepository _repository;
        protected IMapper _mapper;

        public EstimacionesController(EstimacionesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EstimacionesDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Estimaciones>(entity));
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
            IEnumerable<Estimaciones> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<EstimacionesDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Estimaciones result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<EstimacionesDTO>(result));
        }

        [HttpPut]
        public async Task Update(EstimacionesDTO entity)
        {
            await _repository.Update(_mapper.Map<Estimaciones>(entity));
        }
    }
}
