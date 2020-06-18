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
    public class ClinicController : ControllerBase
    {
        private readonly ILogger<ClinicController> _logger;
        private readonly IClinicRepository<Clinic> _clinicRepository;

        public ClinicController(IClinicRepository<Clinic> clinicRepository, ILogger<ClinicController> logger)
        {
            _clinicRepository = clinicRepository;
            _logger = logger;
        }

        // GET: api/Clinic
        [HttpGet]
        public async Task<ApiResult<List<Clinic>>> GetAsync()
        {
            _logger.LogInformation("Method Called - api/Clinic/Get");
            ApiResult<List<Clinic>> response = await _clinicRepository.List();
            return response;
        }

        // GET: api/Clinic/5
        [HttpGet("{id}", Name = "GetClinic")]
        public async Task<ApiResult<Clinic>> GetAsync(int Id)
        {
            _logger.LogInformation("Method Called - api/Clinic/Get");
            ApiResult<Clinic> response = await _clinicRepository.Get(Id);

            _logger.LogInformation("api/Clinic/Get", response);
            return response;
        }

        // POST: api/Clinic
        [HttpPost]
        public async Task<ApiResult<Clinic>> Post(Clinic model)
        {
            _logger.LogInformation("Method Called - api/Clinic/Post");
            ApiResult<Clinic> response = await _clinicRepository.Add(model);
            return response;
        }

        // PUT: api/Clinic/5
        [HttpPut()]
        public async Task<ApiResult<Clinic>> PutAsync(Clinic model)
        {
            _logger.LogInformation("Method Called - api/Clinic/Put");
            ApiResult<Clinic> response = await _clinicRepository.Edit(model);
            return response;
        }

        // DELETE: api/Clinic/5
        [HttpDelete("{id}")]
        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            _logger.LogInformation("Method Called - api/Clinic/Delete");
            ApiResult<bool> response = await _clinicRepository.Delete(id);
            return response;
        }
    }
}
