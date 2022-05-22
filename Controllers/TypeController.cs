using AutoMapper;
using FM_API.Persistance.Repositories.Shared;
using FMAPI.DTOS;
using FMAPI.Helpers;
using FMAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;
using Type = FMAPI.Entities.Type;

namespace FMAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeController : ControllerBase, IGenericCRUD<TypeDTO>
    {
        protected TypeRepository _repository;
        protected IMapper _mapper;

        public TypeController(TypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeDTO entity)
        {
            try
            {
                Type result = await _repository.Create(_mapper.Map<Type>(entity));
                ResponseHelper<TypeDTO> response = new(MessageHelper.SuccessMessage.MaCreate, _mapper.Map<TypeDTO>(result));
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
                IEnumerable<Type> result = await _repository.GetAll();
                ResponseHelper<IEnumerable<TypeDTO>> response = new("", _mapper.Map<IEnumerable<TypeDTO>>(result.ToList()));
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
                Type result = await _repository.Get(item => item.Id == id);
                ResponseHelper<TypeDTO> response = new("", _mapper.Map<TypeDTO>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(TypeDTO entity)
        {
            try
            {
                await _repository.Update(_mapper.Map<Type>(entity));
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
