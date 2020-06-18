using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Medyana.Contract;
using Medyana.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using Medyana.ResourceManager;
using Medyana.Core.Extensions;

namespace Medyana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IEquipmentRepository<Equipment> _equipmentRepository; 
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(IEquipmentRepository<Equipment> equipmentRepository, ILogger<EquipmentController> logger, IStringLocalizer<SharedResources> localizer)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
            _localizer = localizer;
            _logger.LogInformation(_localizer["LogClassConstructor", "EquipmentController"]);
        }

        // GET: api/Equipment
        [HttpGet]
        public async Task<ApiResult<List<Equipment>>> GetAsync()
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Equipment/Get"]);
            ApiResult<List<Equipment>> response = await _equipmentRepository.List();
            _logger.LogInformation(_localizer["LogMethodResult", "api/Equipment/Get", response.Deserialize()]);


            return response;
        }

        // GET: api/Equipment/5
        [HttpGet("{id}", Name = "GetEquipment")]
        public async Task<ApiResult<Equipment>> GetAsync(int Id)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Equipment/Get"]);
            ApiResult<Equipment> response = await _equipmentRepository.Get(Id);
            _logger.LogInformation(_localizer["LogMethodResult", "api/Equipment/Get", response.Deserialize()]);
            return response;
        }

        // POST: api/Equipment
        [HttpPost]
        public async Task<ApiResult<Equipment>> PostAsync(Equipment model)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Equipment/Post"]);
            ApiResult<Equipment> response = await _equipmentRepository.Add(model);
            _logger.LogInformation(_localizer["LogMethodResult", "api/Equipment/Post", response.Deserialize()]);
            return response;
        }

        // PUT: api/Equipment
        [HttpPut()]
        public async Task<ApiResult<Equipment>> PutAsync(Equipment model)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Equipment/Put"]);
            ApiResult<Equipment> response = await _equipmentRepository.Edit(model);
            _logger.LogInformation(_localizer["LogMethodResult", "api/Equipment/Put", response.Deserialize()]);
            return response;
        }

        // DELETE: api/Equipment/5
        [HttpDelete("{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Equipment/Delete"]);
            ApiResult<bool> response = await _equipmentRepository.Delete(id);
            _logger.LogInformation(_localizer["LogMethodResult", "api/Equipment/Delete", response.Deserialize()]);
            
            return response;
        }
    }
}
