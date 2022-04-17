using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetController : ControllerBase, IGenericCRUD<BudgetDTO>
    {
        protected BudgetRepository _repository;
        protected IMapper _mapper;

        public BudgetController(BudgetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetDTO entity)
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
            return Ok(_mapper.Map<IEnumerable<BudgetDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Budget result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<BudgetDTO>(result));
        }

        [HttpPut]
        public async Task Update(BudgetDTO entity)
        {
            await _repository.Update(_mapper.Map<Budget>(entity));
        }
    }
}
