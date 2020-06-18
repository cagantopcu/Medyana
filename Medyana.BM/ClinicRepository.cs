using Medyana.BM.DbObject;
using Medyana.Contract;
using Medyana.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.BM
{
    public class ClinicRepository : IClinicRepository<Clinic>
    {
        private readonly ILogger<ClinicRepository> _logger;

        public ClinicRepository(ILogger<ClinicRepository> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adds New Clinic
        /// </summary>
        /// <param name="value">Clinic Item</param>
        /// <returns>Clinic Item</returns>
        public async Task<ApiResult<Clinic>> Add(Clinic value)
        {
            _logger.LogInformation("Method Called - ClinicRepository/Add");

            ApiResult<Clinic> response = new ApiResult<Clinic>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    ClinicDbObject clinicDbObject = new ClinicDbObject()
                    {
                        Id = value.Id,
                        Name = value.Name
                    };


                    dbContext.ClinicsDbSet.Add(clinicDbObject);
                    await dbContext.SaveChangesAsync();
                    value.Id = clinicDbObject.Id;
                }
                response.Result = value;
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ClinicRepository/Add - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("ClinicRepository/Add - IsSucceed: {0}", response.IsSucceed));
            return response;
        }

        /// <summary>
        /// Edits Clinic
        /// </summary>
        /// <param name="value">Current Clinic Item</param>
        /// <returns>Updated Clinic Item</returns>
        public async Task<ApiResult<Clinic>> Edit(Clinic value)
        {
            _logger.LogInformation("Method Called - ClinicRepository/Edit");
            ApiResult<Clinic> response = new ApiResult<Clinic>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var clinicRecord = await dbContext.ClinicsDbSet.Where(m => m.Id == value.Id).FirstOrDefaultAsync();
                    // TO DO Nullcheck
                    clinicRecord.Name = value.Name;
                    dbContext.Attach(clinicRecord);
                    await dbContext.SaveChangesAsync();
                }

                response.Result = value;
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ClinicRepository/Edit - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("ClinicRepository/Edit - IsSucceed: {0}", response.IsSucceed));
            return response;

        }

        /// <summary>
        /// Gets Clinic Item
        /// </summary>
        /// <param name="Id">Unique Identifier Of Clinic</param>
        /// <returns>Matched Clinic Item</returns>
        public async Task<ApiResult<Clinic>> Get(int Id)
        {
            _logger.LogInformation("Method Called - ClinicRepository/Get");

            ApiResult<Clinic> response = new ApiResult<Clinic>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {

                    var result = await dbContext.ClinicsDbSet.Where(m => m.Id == Id).FirstOrDefaultAsync();

                    response.Result = new Clinic()
                    {
                        Id = result.Id,
                        Name = result.Name
                    };
                }
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ClinicRepository/Get - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("ClinicRepository/Get - IsSucceed: {0}", response.IsSucceed));
            return response;
        }

        /// <summary>
        /// Lists All Clinics
        /// </summary>
        /// <returns>Defined All Clinis</returns>
        public async Task<ApiResult<List<Clinic>>> List()
        {
            _logger.LogInformation("Method Called - ClinicRepository/List");
            ApiResult<List<Clinic>> response = new ApiResult<List<Clinic>>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var result = await dbContext.ClinicsDbSet.ToListAsync();

                    //// TODO mapper
                    response.Result = result.Select(m => new Clinic()
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList();
                }
                response.IsSucceed = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ClinicRepository/List - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("ClinicRepository/Get - IsSucceed: {0}", response.IsSucceed));
            return response;

        }

        /// <summary>
        /// Deletes Record By Passed Id 
        /// </summary>
        /// <param name="Id">Unique Identifier Of Record</param>
        /// <returns>Is Deleted</returns>
        public async Task<ApiResult<bool>> Delete(int Id)
        {
            _logger.LogInformation("Method Called - ClinicRepository/Delete");

            ApiResult<bool> response = new ApiResult<bool>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    ClinicDbObject record = new ClinicDbObject() { Id = Id };
                    dbContext.Attach(record);
                    dbContext.Remove(record);

                    response.Result = await dbContext.SaveChangesAsync() > 0;
                    response.IsSucceed = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("ClinicRepository/Delete - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("ClinicRepository/Delete - IsSucceed: {0}", response.IsSucceed));

            return response;
        }

    }
}
