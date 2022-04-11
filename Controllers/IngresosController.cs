﻿using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngresosController : ControllerBase, IGenericCRUD<IngresosDTO>
    {
        protected IngresosRepository _repository;
        protected IMapper _mapper;

        public IngresosController(IngresosRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IngresosDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Ingresos>(entity));
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
            IEnumerable<Ingresos> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<IngresosDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Ingresos result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<IngresosDTO>(result));
        }

        [HttpPut]
        public async Task Update(IngresosDTO entity)
        {
            await _repository.Update(_mapper.Map<Ingresos>(entity));
        }
    }
}
