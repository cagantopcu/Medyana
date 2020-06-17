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
    public class ClinicController : ControllerBase
    {
        private readonly IClinicRepository<Clinic> _clinicRepository;

        public ClinicController(IClinicRepository<Clinic> clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        // GET: api/Clinic
        [HttpGet]
        public ApiResult<List<Clinic>> Get()
        {
            ApiResult<List<Clinic>> response = _clinicRepository.List();

            return response;
        }

        // GET: api/Clinic/5
        [HttpGet("{id}", Name = "GetClinic")]
        public ApiResult<Clinic> Get(int Id)
        {
            ApiResult<Clinic> response = _clinicRepository.Get(Id);            
            return response;
        }

        // POST: api/Clinic
        [HttpPost]
        public ApiResult<Clinic> Post(Clinic model)
        {
            ApiResult<Clinic> response = _clinicRepository.Add(model);
            return response;
        }

        // PUT: api/Clinic/5
        [HttpPut()]
        public ApiResult<Clinic> Put(Clinic model)
        {
            ApiResult<Clinic> response = _clinicRepository.Edit(model);
            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
