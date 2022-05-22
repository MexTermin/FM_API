using AutoMapper;
using FM_API.DTOS;
using FMAPI.Helpers;
using FMAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        protected TransactionRepository _repository;
        protected CategoryRepository _categoryRepository;
        protected IMapper _mapper;

        public TransactionController(
            TransactionRepository repository,
            IMapper mapper,
            CategoryRepository categoryRepository
            )
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionRequestDTO entity)
        {
            try
            {
                var result = await _repository.Create(_mapper.Map<Transaction>(entity));
                // Category
                result.Category = await _categoryRepository.GetWithDelete(item => item.Id == result.Id_category);

                ResponseHelper<TransactionDTO> response = new(MessageHelper.SuccessMessage.FeCreate, _mapper.Map<TransactionDTO>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long idEntity)
        {
            try
            {
                await _repository.Delete(idEntity);
                ResponseHelper response = new(MessageHelper.SuccessMessage.FeDelete);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);

            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Transaction> result = await _repository.GetAll();
                foreach (Transaction item in result)
                {
                    item.Category = await _categoryRepository.GetWithDelete(e => e.Id == item.Id_category);
                }
                ResponseHelper<IEnumerable<TransactionDTO>> response = new("", _mapper.Map<IEnumerable<TransactionDTO>>(result.ToList()));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                Transaction result = await _repository.GetWithDelete(item => item.Id == id);
                if (result != null)
                {
                    result.Category = await _categoryRepository.GetWithDelete(e => e.Id == result.Id_category);
                }
                ResponseHelper<TransactionDTO> response = new("", _mapper.Map<TransactionDTO>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(TransactionDTO entity)
        {
            try
            {
                await _repository.Update(_mapper.Map<Transaction>(entity));
                ResponseHelper response = new(MessageHelper.SuccessMessage.FeUpdated);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpGet("budget/{id:long}")]
        public async Task<IActionResult> GetByBudget(long id)
        {
            try
            {
                IEnumerable<Transaction> result = await _repository.GetManyWithDelete(e => e.Id_budget == id);
                foreach (Transaction item in result)
                {
                    item.Category = await _categoryRepository.GetWithDelete(e => e.Id == item.Id_category);
                }
                ResponseHelper<IEnumerable<TransactionDTO>> response = new("", _mapper.Map<IEnumerable<TransactionDTO>>(result.ToList()));
                return Ok(response);
            }
            catch
            {
                throw;
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }
    }
}
