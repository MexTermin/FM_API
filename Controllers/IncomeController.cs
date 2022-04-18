using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;
using FMAPI.Helpers;

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
            try
            {
                Income result = await _repository.Create(_mapper.Map<Income>(entity));
                ResponseHelper<IncomeDTO> response = new(MessageHelper.SuccessMessage.MaCreate, _mapper.Map<IncomeDTO>(result));
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
                ResponseHelper response = new(MessageHelper.SuccessMessage.MaDelete);
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
                IEnumerable<Income> result = await _repository.GetAll();
                ResponseHelper<IEnumerable<IncomeDTO>> response = new("", _mapper.Map<IEnumerable<IncomeDTO>>(result.ToList()));
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
                Income result = await _repository.Get(item => item.Id == id);
                ResponseHelper<IncomeDTO> response = new("", _mapper.Map<IncomeDTO>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(IncomeDTO entity)
        {
            try
            {
                await _repository.Update(_mapper.Map<Income>(entity));
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
