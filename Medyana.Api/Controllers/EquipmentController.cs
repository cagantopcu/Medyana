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
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(IEquipmentRepository<Equipment> equipmentRepository, ILogger<EquipmentController> logger)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }

        // GET: api/Equipment
        [HttpGet]
        public async Task<ApiResult<List<Equipment>>> GetAsync()
        {
            _logger.LogInformation("Method Called - api/Equipment/Get");
            ApiResult<List<Equipment>> response = await _equipmentRepository.List();

            return response;
        }

        // GET: api/Equipment/5
        [HttpGet("{id}", Name = "GetEquipment")]
        public async Task<ApiResult<Equipment>> GetAsync(int Id)
        {
            _logger.LogInformation("Method Called - api/Equipment/Get");
            ApiResult<Equipment> response = await _equipmentRepository.Get(Id);
            return response;
        }

        // POST: api/Equipment
        [HttpPost]
        public async Task<ApiResult<Equipment>> PostAsync(Equipment model)
        {
            _logger.LogInformation("Method Called - api/Equipment/Post");
            ApiResult<Equipment> response = await _equipmentRepository.Add(model);
            return response;
        }

        // PUT: api/Equipment/5
        [HttpPut("{id}")]
        public async Task<ApiResult<Equipment>> PutAsync(Equipment model)
        {
            _logger.LogInformation("Method Called - api/Equipment/Put");
            ApiResult<Equipment> response = await _equipmentRepository.Edit(model);
            return response;
        }

        // DELETE: api/Equipment/5
        [HttpDelete("{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            _logger.LogInformation("Method Called - api/Equipment/Delete");
            ApiResult<bool> response = await _equipmentRepository.Delete(id);
            return response;
        }
    }
}
