using Medyana.BM.DbObject;
using Medyana.Contract;
using Medyana.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Medyana.ResourceManager;
using Medyana.Core.Extensions;

namespace Medyana.BM
{
    public class ClinicRepository : IClinicRepository<Clinic>
    {
        private readonly ILogger<ClinicRepository> _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;


        public ClinicRepository(ILogger<ClinicRepository> logger, IStringLocalizer<SharedResources> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            _logger.LogInformation(_localizer["LogClassConstructor", "ClinicRepository"]);
        }

        /// <summary>
        /// Adds New Clinic
        /// </summary>
        /// <param name="value">Clinic Item</param>
        /// <returns>Clinic Item</returns>
        public async Task<ApiResult<Clinic>> Add(Clinic value)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "ClinicRepository/Add"]);

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
                _logger.LogInformation(_localizer["LogMethodError", "ClinicRepository/Add", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(_localizer["LogMethodSucceed", "ClinicRepository/Add", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "ClinicRepository/Add", response.Deserialize()]);

            return response;
        }

        /// <summary>
        /// Edits Clinic
        /// </summary>
        /// <param name="value">Current Clinic Item</param>
        /// <returns>Updated Clinic Item</returns>
        public async Task<ApiResult<Clinic>> Edit(Clinic value)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "ClinicRepository/Edit"]);
            ApiResult<Clinic> response = new ApiResult<Clinic>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var clinicRecord = await dbContext.ClinicsDbSet.Where(m => m.Id == value.Id).FirstOrDefaultAsync();

                    if (clinicRecord == null)
                    {
                        response.ErrorMessage = _localizer["RecordNotFound" , "Clinic"].Value;
                        _logger.LogInformation(_localizer["LogErrorMessage", "ClinicRepository/Edit", response.ErrorMessage]);
                        return response;
                    }

                    clinicRecord.Name = value.Name;
                    dbContext.Attach(clinicRecord);
                    await dbContext.SaveChangesAsync();
                }

                response.Result = value;
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(_localizer["LogMethodError", "ClinicRepository/Edit", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(_localizer["LogMethodSucceed", "ClinicRepository/Edit", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "ClinicRepository/Edit", response.Deserialize()]);
            return response;

        }

        /// <summary>
        /// Gets Clinic Item
        /// </summary>
        /// <param name="Id">Unique Identifier Of Clinic</param>
        /// <returns>Matched Clinic Item</returns>
        public async Task<ApiResult<Clinic>> Get(int Id)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "ClinicRepository/Get"]);

            ApiResult<Clinic> response = new ApiResult<Clinic>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {

                    var result = await dbContext.ClinicsDbSet.Where(m => m.Id == Id).FirstOrDefaultAsync();

                    if (result == null)
                    {
                        response.ErrorMessage = _localizer["RecordNotFound", "Clinic"].Value;
                        _logger.LogInformation(_localizer["LogErrorMessage", "ClinicRepository/Get", response.ErrorMessage]);
                        return response;
                    }

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
                _logger.LogInformation(_localizer["LogMethodError", "ClinicRepository/Get", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }
            _logger.LogInformation(_localizer["LogMethodSucceed", "ClinicRepository/Get", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "ClinicRepository/Get", response.Deserialize()]);
            return response;
        }

        /// <summary>
        /// Lists All Clinics
        /// </summary>
        /// <returns>Defined All Clinis</returns>
        public async Task<ApiResult<List<Clinic>>> List()
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "ClinicRepository/List"]);
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
                _logger.LogInformation(_localizer["LogMethodError", "ClinicRepository/List", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(_localizer["LogMethodSucceed", "ClinicRepository/List", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "ClinicRepository/List", response.Deserialize()]);
            return response;

        }

        /// <summary>
        /// Deletes Record By Passed Id 
        /// </summary>
        /// <param name="Id">Unique Identifier Of Record</param>
        /// <returns>Is Deleted</returns>
        public async Task<ApiResult<bool>> Delete(int Id)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "ClinicRepository/Delete"]);

            ApiResult<bool> response = new ApiResult<bool>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    if (dbContext.ClinicsDbSet.Any(m => m.Id == Id) == false)
                    {
                        response.ErrorMessage = _localizer["RecordNotFound" , "Clinic"].Value;
                        _logger.LogInformation(_localizer["LogErrorMessage", "ClinicRepository/Delete", response.ErrorMessage]);
                        return response;
                    }

                    ClinicDbObject record = new ClinicDbObject() { Id = Id };
                    dbContext.Attach(record);
                    dbContext.Remove(record);

                    response.Result = await dbContext.SaveChangesAsync() > 0;
                    response.IsSucceed = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(_localizer["LogMethodError", "ClinicRepository/Delete", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }
            _logger.LogInformation(_localizer["LogMethodSucceed", "ClinicRepository/Delete", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "ClinicRepository/Delete", response.Deserialize()]);

            return response;
        }

    }
}
