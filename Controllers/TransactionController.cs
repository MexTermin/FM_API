using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase, IGenericCRUD<TransactionDTO>
    {
        protected TransactionRepository _repository;
        protected IMapper _mapper;

        public TransactionController(TransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Transaction>(entity));
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
            IEnumerable<Transaction> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<TransactionDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Transaction result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<TransactionDTO>(result));
        }

        [HttpPut]
        public async Task Update(TransactionDTO entity)
        {
            await _repository.Update(_mapper.Map<Transaction>(entity));
        }
    }
}
