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
using Newtonsoft.Json;
using Medyana.ResourceManager;
using Medyana.Core.Extensions;

namespace Medyana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ILogger<ClinicController> _logger;
        private readonly IClinicRepository<Clinic> _clinicRepository;

        public ClinicController(IClinicRepository<Clinic> clinicRepository, ILogger<ClinicController> logger, IStringLocalizer<SharedResources> localizer)
        {
            _clinicRepository = clinicRepository;
            _logger = logger;
            _localizer = localizer;
            _logger.LogInformation(_localizer["LogClassConstructor", "ClinicController"]);
        }

        // GET: api/Clinic
        [HttpGet]
        public async Task<ApiResult<List<Clinic>>> GetAsync()
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Clinic/Get"]);
            ApiResult<List<Clinic>> response = await _clinicRepository.List();
            _logger.LogInformation(_localizer["LogMethodResult", "api/Clinic/Get", response.Deserialize()]);
            return response;
        }

        // GET: api/Clinic/5
        [HttpGet("{id}", Name = "GetClinic")]
        public async Task<ApiResult<Clinic>> GetAsync(int Id)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Clinic/Get"]);
            ApiResult<Clinic> response = await _clinicRepository.Get(Id);
            _logger.LogInformation(_localizer["LogMethodResult", "api/Clinic/Get", response.Deserialize()]);
            return response;
        }

        // POST: api/Clinic
        [HttpPost]
        public async Task<ApiResult<Clinic>> Post(Clinic model)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Clinic/Post"]);
            ApiResult<Clinic> response = await _clinicRepository.Add(model);
            _logger.LogInformation(_localizer["LogMethodResult", "api/Clinic/Post", response.Deserialize()]);
            return response;
        }

        // PUT: api/Clinic/5
        [HttpPut()]
        public async Task<ApiResult<Clinic>> PutAsync(Clinic model)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Clinic/Put"]);
            ApiResult<Clinic> response = await _clinicRepository.Edit(model);
            _logger.LogInformation(_localizer["LogMethodResult", "api/Clinic/Put", response.Deserialize()]);
            return response;
        }

        // DELETE: api/Clinic/5
        [HttpDelete("{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "api/Clinic/Delete"]);
            ApiResult<bool> response = await _clinicRepository.Delete(id);
            _logger.LogInformation(_localizer["LogMethodResult", "api/Clinic/Delete", response.Deserialize()]);
            return response;
        }
    }
}
