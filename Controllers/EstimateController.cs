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
    public class EstimateController : ControllerBase
    {
        protected EstimateRepository _repository;
        protected CategoryRepository _categoryRepository;
        protected EstimateSpentRepository _ESpentRepository;
        protected EstimateIncomeRepository _EIncomeRepository;
        protected IncomeRepository _incomeRepository;
        protected SpentRepository _spentRepository;
        protected IMapper _mapper;

        public EstimateController(
            EstimateRepository repository,
            IMapper mapper,
            CategoryRepository categoryRepository,
            SpentRepository spentRepository,
            EstimateIncomeRepository EIncomeRepository,
            EstimateSpentRepository ESRepository,
            IncomeRepository incomeRepository
            )
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _EIncomeRepository = EIncomeRepository;
            _ESpentRepository = ESRepository;
            _incomeRepository = incomeRepository;
            _spentRepository = spentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EstimateRequestDTO entity)
        {
            try
            {
                var result = await _repository.Create(_mapper.Map<Estimate>(entity));
                // Category
                result.Category = await _categoryRepository.GetWithDelete(item => item.Id == result.Id_category);


                // Create spent and relations
                var newSpent = await _spentRepository.Create(_mapper.Map<Spent>(entity.Spent));
                var newEstimateSpent = await _ESpentRepository.Create(new EstimateSpent { Id_Estimate = result.Id, Id_Spent = newSpent.Id });
                newEstimateSpent.Spent = newSpent;
                result.Expenses = new List<EstimateSpent>() { newEstimateSpent };

                // Create income and relations
                var newIncome = await _incomeRepository.Create(_mapper.Map<Income>(entity.SingleIncome));
                var newEstimateSIncome = await _EIncomeRepository.Create(new EstimateIncome { Id_Estimate = result.Id, Id_Income = newIncome.Id });
                newEstimateSIncome.Income = newIncome;
                result.Income = new List<EstimateIncome>() { newEstimateSIncome };

                ResponseHelper<EstimateDTO> response = new(MessageHelper.SuccessMessage.FeCreate, _mapper.Map<EstimateDTO>(result));
                return Ok(response);
            }
            catch
            {
                throw;
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
                await _EIncomeRepository.DeleteAll(item => item.Id_Estimate == idEntity);
                await _ESpentRepository.DeleteAll(item => item.Id_Estimate == idEntity);
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
                IEnumerable<Estimate> result = await _repository.GetAll();
                foreach (Estimate item in result)
                {
                    item.Category = await _categoryRepository.GetWithDelete(e => e.Id == item.Id_category);
                    item.Income = await GetEstimateIncomes(item.Id);
                    item.Expenses = await GetEstimateSpent(item.Id);
                }
                ResponseHelper<IEnumerable<EstimateDTO>> response = new("", _mapper.Map<IEnumerable<EstimateDTO>>(result.ToList()));
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
                Estimate result = await _repository.GetWithDelete(item => item.Id == id);
                if(result != null)
                {
                    result.Category = await _categoryRepository.GetWithDelete(e => e.Id == result.Id_category);
                    result.Income = await GetEstimateIncomes(result.Id);
                    result.Expenses = await GetEstimateSpent(result.Id);
                }
                ResponseHelper<EstimateDTO> response = new("", _mapper.Map<EstimateDTO>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(EstimateDTO entity)
        {
            try
            {
                await _repository.Update(_mapper.Map<Estimate>(entity));
                ResponseHelper response = new(MessageHelper.SuccessMessage.FeUpdated);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPost("spent")]
        public async Task<IActionResult> AddSpent(EstimateSpentDTO entity)
        {
            try
            {
                var result = await _ESpentRepository.Create(_mapper.Map<EstimateSpent>(entity));
                ResponseHelper response = new(MessageHelper.SuccessMessage.MaCreate);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPost("income")]
        public async Task<IActionResult> AddIncome(EstimateIncomeDTO entity)
        {
            try
            {
                await _EIncomeRepository.Create(_mapper.Map<EstimateIncome>(entity));
                ResponseHelper response = new(MessageHelper.SuccessMessage.MaCreate);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpGet("income/{id:long}")]
        public async Task<IActionResult> GetAllIncome(long id)
        {
            try
            {
                var result = await _EIncomeRepository.GetManyWithDelete(e => e.Id_Estimate == id);
                foreach (var resultItem in result)
                {
                    resultItem.Income = await _incomeRepository.GetWithDelete(item => item.Id == resultItem.Id_Income);
                }

                ResponseHelper<IEnumerable<EstimateIncomeDTO>> response = new("", _mapper.Map<IEnumerable<EstimateIncomeDTO>>(result.ToList()));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpGet("spent/{id:long}")]
        public async Task<IActionResult> GetAllSpent(long id)
        {
            try
            {
                var result = await _ESpentRepository.GetManyWithDelete(e => e.Id_Estimate == id);
                foreach (var resultItem in result)
                {
                    resultItem.Spent = await _spentRepository.GetWithDelete(item => item.Id == resultItem.Id_Spent);
                }

                ResponseHelper<IEnumerable<EstimateSpentDTO>> response = new("", _mapper.Map<IEnumerable<EstimateSpentDTO>>(result.ToList()));
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
                IEnumerable<Estimate> result = await _repository.GetManyWithDelete(e => e.Id_budget == id);
                foreach (Estimate item in result)
                {
                    item.Category = await _categoryRepository.GetWithDelete(e => e.Id == item.Id_category);
                    item.Income = await GetEstimateIncomes(item.Id);
                    item.Expenses = await GetEstimateSpent(item.Id);
                }
                ResponseHelper<IEnumerable<EstimateDTO>> response = new("", _mapper.Map<IEnumerable<EstimateDTO>>(result.ToList()));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        protected async Task<List<EstimateIncome>> GetEstimateIncomes(long id)
        {
            var result = (await _EIncomeRepository.GetManyWithDelete(e => e.Id_Estimate == id)).ToList();

            foreach (var item in result)
            {
                item.Income = await _incomeRepository.GetWithDelete(e => e.Id == item.Id_Income);
            }

            return result;
        }

        protected async Task<List<EstimateSpent>> GetEstimateSpent(long id)
        {
            var result = (await _ESpentRepository.GetManyWithDelete(e => e.Id_Estimate == id)).ToList();

            foreach (var item in result)
            {
                item.Spent = await _spentRepository.GetWithDelete(e => e.Id == item.Id_Spent);
            }

            return result;
        }
    }
}
