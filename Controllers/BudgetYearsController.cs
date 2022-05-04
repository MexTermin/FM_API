using AutoMapper;
using FM_API.Persistance.Repositories.Shared;
using FMAPI.DTOS;
using FMAPI.Helpers;
using FMAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FMAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetYearsController : ControllerBase, IGenericCRUD<BudgetYearsDTO>
    {
        protected BudgetYearsRepository _repository;
        protected IMapper _mapper;
        protected BudgetRepository _budgetRepository;

        public BudgetYearsController(BudgetYearsRepository repository, IMapper mapper, BudgetRepository budgetRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _budgetRepository = budgetRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetYearsDTO entity)
        {
            try
            {
                BudgetYears budget = await _repository.Get(item => item.Year == entity.Year);
                if (entity.Year < DateTime.UtcNow.Year) return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.IncorrectYear, error: true));
                if (budget != null) return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.YearAlreadyExits, error: true));

                var result = await _repository.Create(_mapper.Map<BudgetYears>(entity));
                ResponseHelper<BudgetYearsDTO> response = new(MessageHelper.SuccessMessage.MaCreate, _mapper.Map<BudgetYearsDTO>(result));
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
                IEnumerable<BudgetYears> result = await _repository.GetAll();
                foreach(var budget in result)
                {
                    budget.Budgets = (await _budgetRepository.GetManyWithDelete(item => item.Id_budgetYear == budget.Id)).ToList();
                }
                ResponseHelper<IEnumerable<BudgetYearsDTO>> response = new("", _mapper.Map<IEnumerable<BudgetYearsDTO>>(result.ToList()));
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
                BudgetYears result = await _repository.GetWithDelete(item => item.Id == id);
                result.Budgets = (await _budgetRepository.GetManyWithDelete(item => item.Id_budgetYear == result.Id)).ToList();
                ResponseHelper<BudgetYearsDTO> response = new("", _mapper.Map<BudgetYearsDTO>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(BudgetYearsDTO entity)
        {
            try
            {
                await _repository.Update(_mapper.Map<BudgetYears>(entity));
                ResponseHelper response = new(MessageHelper.SuccessMessage.MaUpdated);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }
    }
}
