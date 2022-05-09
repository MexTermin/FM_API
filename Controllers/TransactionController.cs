using AutoMapper;
using FM_API.DTOS;
using FMAPI.DTOS;
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
        protected TransactionSpentRepository _TSpentRepository;
        protected TransactionIncomeRepository _TIncomeRepository;
        protected IncomeRepository _incomeRepository;
        protected SpentRepository _spentRepository;
        protected IMapper _mapper;

        public TransactionController(
            TransactionRepository repository,
            IMapper mapper,
            CategoryRepository categoryRepository,
            SpentRepository spentRepository,
            TransactionIncomeRepository TIncomeRepository,
            TransactionSpentRepository TSRepository,
            IncomeRepository incomeRepository
            )
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _TIncomeRepository = TIncomeRepository;
            _TSpentRepository = TSRepository;
            _incomeRepository = incomeRepository;
            _spentRepository = spentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionRequestDTO entity)
        {
            try
            {
                var result = await _repository.Create(_mapper.Map<Transaction>(entity));
                // Category
                result.Category = await _categoryRepository.GetWithDelete(item => item.Id == result.Id_category);
                result.Expenses = new List<TransactionSpent>();
                result.Income = new List<TransactionIncome>();

                // Create spent and relations
                foreach (SpentDTO spent in entity.Expenses)
                {
                    var newSpent = await _spentRepository.Create(_mapper.Map<Spent>(spent));
                    var newTransactionSpent = await _TSpentRepository.Create(new TransactionSpent { Id_Transaction = result.Id, Id_Spent = newSpent.Id });
                    newTransactionSpent.Spent = newSpent;
                }

                // Create income and relations
                foreach (IncomeDTO income in entity.Income)
                {
                    var newIncome = await _incomeRepository.Create(_mapper.Map<Income>(income));
                    var newTransactionIncome = await _TIncomeRepository.Create(new TransactionIncome { Id_Transaction = result.Id, Id_Income = newIncome.Id });
                    newTransactionIncome.Income = newIncome;
                }

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
                await _TIncomeRepository.DeleteAll(item => item.Id_Transaction == idEntity);
                await _TSpentRepository.DeleteAll(item => item.Id_Transaction == idEntity);
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
                    item.Income = await GetEstimateIncomes(item.Id);
                    item.Expenses = await GetEstimateSpent(item.Id);
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
                    result.Income = await GetEstimateIncomes(result.Id);
                    result.Expenses = await GetEstimateSpent(result.Id);
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

        [HttpPost("spent")]
        public async Task<IActionResult> AddSpent(TransactionSpentDTO entity)
        {
            try
            {
                var result = await _TSpentRepository.Create(_mapper.Map<TransactionSpent>(entity));
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
        public async Task<IActionResult> AddIncome(TransactionIncomeDTO entity)
        {
            try
            {
                await _TIncomeRepository.Create(_mapper.Map<TransactionIncome>(entity));
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
                var result = await _TIncomeRepository.GetManyWithDelete(e => e.Id_Transaction == id);
                foreach (var resultItem in result)
                {
                    resultItem.Income = await _incomeRepository.GetWithDelete(item => item.Id == resultItem.Id_Income);
                }

                ResponseHelper<IEnumerable<TransactionIncomeDTO>> response = new("", _mapper.Map<IEnumerable<TransactionIncomeDTO>>(result.ToList()));
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
                var result = await _TSpentRepository.GetManyWithDelete(e => e.Id_Transaction == id);
                foreach (var resultItem in result)
                {
                    resultItem.Spent = await _spentRepository.GetWithDelete(item => item.Id == resultItem.Id_Spent);
                }

                ResponseHelper<IEnumerable<TransactionSpentDTO>> response = new("", _mapper.Map<IEnumerable<TransactionSpentDTO>>(result.ToList()));
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
                    item.Income = await GetEstimateIncomes(item.Id);
                    item.Expenses = await GetEstimateSpent(item.Id);
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

        protected async Task<List<TransactionIncome>> GetEstimateIncomes(long id)
        {
            var result = (await _TIncomeRepository.GetManyWithDelete(e => e.Id_Transaction == id)).ToList();

            foreach (var item in result)
            {
                item.Income = await _incomeRepository.GetWithDelete(e => e.Id == item.Id_Income);
            }

            return result;
        }

        protected async Task<List<TransactionSpent>> GetEstimateSpent(long id)
        {
            var result = (await _TSpentRepository.GetManyWithDelete(e => e.Id_Transaction == id)).ToList();

            foreach (var item in result)
            {
                item.Spent = await _spentRepository.GetWithDelete(e => e.Id == item.Id_Spent);
            }

            return result;
        }
    }
}
