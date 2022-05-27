using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories.Shared;
using FMAPI.Helpers;
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
            try
            {
                if (entity.Month > 12 || entity.Month <= 0) return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.IncorrectMonth, error: true));
                if (entity.Year < DateTime.UtcNow.Year) return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.IncorrectYear, error: true));

                var previusDateCombination = await ValidDateCombination(entity);
                if (previusDateCombination != null) return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.combinationAlreadyExits, error: true));

                var previusBudget = await ExistingBudget(entity);
                if (previusBudget == null)
                {
                    var result = await _repository.Create(_mapper.Map<Budget>(entity));
                    ResponseHelper<BudgetDTO> response = new(MessageHelper.SuccessMessage.FeCreate, _mapper.Map<BudgetDTO>(result));
                    return Ok(response);
                }
                else if (previusBudget.Deleted_at == null)
                {
                    return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.NameAlreadyExits, error: true));
                }
                else
                {
                    previusBudget.Deleted_at = null;
                    await _repository.Update(previusBudget);
                    ResponseHelper<BudgetDTO> response = new(MessageHelper.SuccessMessage.MaCreate, _mapper.Map<BudgetDTO>(previusBudget));
                    return Ok(response);
                }
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
                return Ok(new ResponseHelper(MessageHelper.SuccessMessage.MaDelete));
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
                IEnumerable<Budget> result = await _repository.GetAll();
                ResponseHelper<IEnumerable<BudgetDTO>> response = new("", _mapper.Map<IEnumerable<BudgetDTO>>(result.ToList()));
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
                Budget result = await _repository.GetWithDelete(item => item.Id == id);
                ResponseHelper<BudgetDTO> response = new("", _mapper.Map<BudgetDTO>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(BudgetDTO entity)
        {
            try
            {
                var previusDateCombination = await ValidDateCombination(entity);
                if (previusDateCombination != null) return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.combinationAlreadyExits, error: true));

                await _repository.Update(_mapper.Map<Budget>(entity));
                ResponseHelper response = new(MessageHelper.SuccessMessage.MaUpdated);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        protected async Task<Budget?> ExistingBudget(BudgetDTO entity)
        {
            Budget result = await _repository.GetWithDelete(item => item.Month == entity.Month);
            ResponseHelper<BudgetDTO> response = new("", _mapper.Map<BudgetDTO>(result));
            return result;
        }

        protected async Task<Budget?> ValidDateCombination(BudgetDTO entity)
        {
            Budget result = await _repository.Get(item => item.Month == entity.Month && item.Year == entity.Year && item.Id != entity.Id);
            ResponseHelper<BudgetDTO> response = new("", _mapper.Map<BudgetDTO>(result));
            return result;
        }

    }
}
