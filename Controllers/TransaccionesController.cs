﻿using AutoMapper;
using FM_API.DTOS;
using FM_API.Persistance.Repositories;
using FM_API.Persistance.Repositories.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransaccionesController : ControllerBase, IGenericCRUD<TransaccionDTO>
    {
        protected TransaccionRepository _repository;
        protected IMapper _mapper;

        public TransaccionesController(TransaccionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransaccionDTO entity)
        {
            var result = await _repository.Create(_mapper.Map<Transaction>(entity));
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
            IEnumerable<Transaction> result = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<TransaccionDTO>>(result.ToList()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            Transaction result = await _repository.Get(item => item.Id == id);
            return Ok(_mapper.Map<TransaccionDTO>(result));
        }

        [HttpPut]
        public async Task Update(TransaccionDTO entity)
        {
            await _repository.Update(_mapper.Map<Transaction>(entity));
        }
    }
}
