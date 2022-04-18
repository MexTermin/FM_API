using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using FMAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpentController : ControllerBase, IGenericCRUD<SpentDTO>
    {
        protected SpentRepository _repository;
        protected IMapper _mapper;

        public SpentController(SpentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpentDTO entity)
        {
            try
            {
                var result = await _repository.Create(_mapper.Map<Spent>(entity));
                ResponseHelper<SpentDTO> response = new(MessageHelper.SuccessMessage.MaCreate, _mapper.Map<SpentDTO>(result));
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
                IEnumerable<Spent> result = await _repository.GetAll();
                ResponseHelper<IEnumerable<Spent>> response = new("", _mapper.Map<IEnumerable<Spent>>(result.ToList()));
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
                Spent result = await _repository.Get(item => item.Id == id);
                ResponseHelper<Spent> response = new("", _mapper.Map<Spent>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(SpentDTO entity)
        {
            try
            {
                await _repository.Update(_mapper.Map<Spent>(entity));
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
