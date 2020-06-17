using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Medyana.Contract;
using Medyana.Model;

namespace Medyana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository<Equipment> _equipmentRepository;

        public EquipmentController(IEquipmentRepository<Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        // GET: api/Equipment
        [HttpGet]
        public ApiResult<List<Equipment>> Get()
        {
            ApiResult<List<Equipment>> response = _equipmentRepository.List();

            return response;
        }

        // GET: api/Equipment/5
        [HttpGet("{id}", Name = "GetEquipment")]
        public ApiResult<Equipment> Get(int Id)
        {
            ApiResult<Equipment> response = _equipmentRepository.Get(Id);
            return response;
        }

        // POST: api/Equipment
        [HttpPost]
        public ApiResult<Equipment> Post(Equipment model)
        {
            ApiResult<Equipment> response = _equipmentRepository.Add(model);
            return response;
        }

        // PUT: api/Equipment/5
        [HttpPut("{id}")]
        public ApiResult<Equipment> Put(Equipment model)
        {
            ApiResult<Equipment> response = _equipmentRepository.Edit(model);
            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
