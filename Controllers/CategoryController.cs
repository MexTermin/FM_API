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
    public class CategoryController : ControllerBase, IGenericCRUD<CategoryDTO>
    {
        protected CategoryRepository _repository;
        protected IMapper _mapper;

        public CategoryController(CategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO entity)
        {
            try
            {
                var previusCategory = await ExistingCategory(entity);
                if (previusCategory == null)
                {
                    var result = await _repository.Create(_mapper.Map<Category>(entity));
                    ResponseHelper<CategoryDTO> response = new(MessageHelper.SuccessMessage.FeCreate, _mapper.Map<CategoryDTO>(result));
                    return Ok(response);
                }
                else if (previusCategory.Deleted == false)
                {
                    return BadRequest(new ResponseHelper(MessageHelper.ErrorMessage.NameAlreadyExits, error: true));
                }
                else
                {
                    previusCategory.Deleted = false;
                    previusCategory.Description = entity.Description;

                    await _repository.Update(previusCategory);
                    ResponseHelper<CategoryDTO> response = new(MessageHelper.SuccessMessage.FeCreate, _mapper.Map<CategoryDTO>(previusCategory));
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
                var category = await _repository.Get(item => item.Id == idEntity); // Search for a category to do the logical deletion. 
                category.Deleted = true;
                await _repository.Update(category);
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
                IEnumerable<Category> result = await _repository.GetMany(item => item.Deleted == false);
                ResponseHelper<IEnumerable<CategoryDTO>> response = new("", _mapper.Map<IEnumerable<CategoryDTO>>(result.ToList()));
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
                Category result = await _repository.Get(item => item.Id == id && item.Deleted == false);
                ResponseHelper<CategoryDTO> response = new("", _mapper.Map<CategoryDTO>(result));
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDTO entity)
        {
            try
            {
                await _repository.Update(_mapper.Map<Category>(entity));
                ResponseHelper response = new(MessageHelper.SuccessMessage.FeUpdated);
                return Ok(response);
            }
            catch
            {
                ResponseHelper response = new(MessageHelper.ErrorMessage.GenericError, error: true);
                return BadRequest(response);
            }
        }

        protected async Task<Category?> ExistingCategory(CategoryDTO entity)
        {
            Category result = await _repository.Get(item => item.Name == entity.Name);
            ResponseHelper<CategoryDTO> response = new("", _mapper.Map<CategoryDTO>(result));
            return result;
        }

    }
}
