﻿using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstimateController : ControllerBase, IGenericCRUD<EstimateDTO>
    {
        protected EstimateRepository _repository;
        protected IMapper _mapper;

        public EstimateController(EstimateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EstimateDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Estimate>(entity));
            return Ok(result);
        }
        [HttpDelete]
        public async Task Delete(long idEntity)
        {
            await _repository.Delete(idEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Estimate> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<EstimateDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Estimate result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<EstimateDTO>(result));
        }

        [HttpPut]
        public async Task Update(EstimateDTO entity)
        {
            await _repository.Update(_mapper.Map<Estimate>(entity));
        }
    }
}