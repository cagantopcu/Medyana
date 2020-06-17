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
        public ApiResult<List<Clinic>> Get()
        {
            _logger.LogInformation("Method Called - api/Clinic/Get");
            ApiResult<List<Clinic>> response = _clinicRepository.List();
            return response;
        }

        // GET: api/Clinic/5
        [HttpGet("{id}", Name = "GetClinic")]
        public ApiResult<Clinic> Get(int Id)
        {
            _logger.LogInformation("Method Called - api/Clinic/Get");
            ApiResult<Clinic> response = _clinicRepository.Get(Id);

            _logger.LogInformation("api/Clinic/Get", response);
            return response;
        }

        // POST: api/Clinic
        [HttpPost]
        public ApiResult<Clinic> Post(Clinic model)
        {
            _logger.LogInformation("Method Called - api/Clinic/Post");
            ApiResult<Clinic> response = _clinicRepository.Add(model);
            return response;
        }

        // PUT: api/Clinic/5
        [HttpPut()]
        public ApiResult<Clinic> Put(Clinic model)
        {
            _logger.LogInformation("Method Called - api/Clinic/Put");
            ApiResult<Clinic> response = _clinicRepository.Edit(model);
            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ApiResult<bool> Delete(int id)
        {
            _logger.LogInformation("Method Called - api/Clinic/Delete");
            ApiResult<bool> response = _clinicRepository.Delete(id);
            return response;
        }
    }
}
