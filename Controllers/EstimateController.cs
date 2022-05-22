using AutoMapper;
using FM_API.DTOS;

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
        protected IMapper _mapper;

        public EstimateController(
            EstimateRepository repository,
            IMapper mapper,
            CategoryRepository categoryRepository
            )
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EstimateRequestDTO entity)
        {
            try
            {
                var result = await _repository.Create(_mapper.Map<Estimate>(entity));
                // Category
                result.Category = await _categoryRepository.GetWithDelete(item => item.Id == result.Id_category);

                ResponseHelper<EstimateDTO> response = new(MessageHelper.SuccessMessage.FeCreate, _mapper.Map<EstimateDTO>(result));
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
                IEnumerable<Estimate> result = await _repository.GetAll();
                foreach (Estimate item in result)
                {
                    item.Category = await _categoryRepository.GetWithDelete(e => e.Id == item.Id_category);
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
                if (result != null)
                {
                    result.Category = await _categoryRepository.GetWithDelete(e => e.Id == result.Id_category);
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

        [HttpGet("budget/{id:long}")]
        public async Task<IActionResult> GetByBudget(long id)
        {
            try
            {
                IEnumerable<Estimate> result = await _repository.GetManyWithDelete(e => e.Id_budget == id);
                foreach (Estimate item in result)
                {
                    item.Category = await _categoryRepository.GetWithDelete(e => e.Id == item.Id_category);
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
    }
}
