using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Medyana.Contract;
using Medyana.Model;
using Microsoft.Extensions.Logging;

namespace Medyana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository<Equipment> _equipmentRepository; 
        private readonly ILogger<ClinicController> _logger;

        public EquipmentController(IEquipmentRepository<Equipment> equipmentRepository, ILogger<ClinicController> logger)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }

        // GET: api/Equipment
        [HttpGet]
        public ApiResult<List<Equipment>> Get()
        {
            _logger.LogInformation("Method Called - api/Equipment/Get");
            ApiResult<List<Equipment>> response = _equipmentRepository.List();

            return response;
        }

        // GET: api/Equipment/5
        [HttpGet("{id}", Name = "GetEquipment")]
        public ApiResult<Equipment> Get(int Id)
        {
            _logger.LogInformation("Method Called - api/Equipment/Get");
            ApiResult<Equipment> response = _equipmentRepository.Get(Id);
            return response;
        }

        // POST: api/Equipment
        [HttpPost]
        public ApiResult<Equipment> Post(Equipment model)
        {
            _logger.LogInformation("Method Called - api/Equipment/Post");
            ApiResult<Equipment> response = _equipmentRepository.Add(model);
            return response;
        }

        // PUT: api/Equipment/5
        [HttpPut("{id}")]
        public ApiResult<Equipment> Put(Equipment model)
        {
            _logger.LogInformation("Method Called - api/Equipment/Put");
            ApiResult<Equipment> response = _equipmentRepository.Edit(model);
            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation("Method Called - api/Equipment/Delete");
        }
    }
}
