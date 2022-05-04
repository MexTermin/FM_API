using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories.Shared;
using FMAPI.DTOS;
using FMAPI.Helpers;
using FMAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetController : ControllerBase, IGenericCRUD<BudgetDTO>
    {
        protected BudgetRepository _repository;
        protected IMapper _mapper;
        protected BudgetYearsRepository _budgetYearsRepository;

        public BudgetController(BudgetRepository repository, IMapper mapper, BudgetYearsRepository budgetYearsRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _budgetYearsRepository = budgetYearsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetDTO entity)
        {
            try
            {
                if (entity.Month > 12 || entity.Month <= 0) return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.IncorrectMonth, error: true));

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
                    previusBudget.Id_budgetYear = entity.Id_budgetYear;
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

    }
}
