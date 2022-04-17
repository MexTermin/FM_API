using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpentController : ControllerBase, IGenericCRUD<SpentDTO>
    {
        protected SpentRepository _repository;
        protected IMapper _mapper;

        public SpentController(SpentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpentDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Spent>(entity));
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
            IEnumerable<Spent> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Spent result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<SpentDTO>(result));
        }

        [HttpPut]
        public async Task Update(SpentDTO entity)
        {
            await _repository.Update(_mapper.Map<Spent>(entity));
        }

    }
}
