using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase, IGenericCRUD<IncomeDTO>
    {
        protected IncomeRepository _repository;
        protected IMapper _mapper;

        public IncomeController(IncomeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IncomeDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Income>(entity));
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
            IEnumerable<Income> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<IncomeDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Income result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<IncomeDTO>(result));
        }

        [HttpPut]
        public async Task Update(IncomeDTO entity)
        {
            await _repository.Update(_mapper.Map<Income>(entity));
        }
    }
}
