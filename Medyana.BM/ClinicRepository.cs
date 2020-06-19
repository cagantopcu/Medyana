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
        private readonly MedyanaDbContext _dbContext;


        public ClinicRepository(ILogger<ClinicRepository> logger, IStringLocalizer<SharedResources> localizer, MedyanaDbContext dbContext)
        {
            _logger = logger;
            _localizer = localizer;
            _dbContext = dbContext;
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
                ClinicDbObject clinicDbObject = new ClinicDbObject()
                {
                    Id = value.Id,
                    Name = value.Name
                };


                _dbContext.ClinicsDbSet.Add(clinicDbObject);
                await _dbContext.SaveChangesAsync();

                value.Id = clinicDbObject.Id;
                response.Result = value;
                response.IsSucceed = true;
                response.SuccessMessage = _localizer["SucceedMessage", _localizer["Add"]];
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
                var clinicRecord = await _dbContext.ClinicsDbSet.Where(m => m.Id == value.Id).FirstOrDefaultAsync();

                if (clinicRecord == null)
                {
                    response.ErrorMessage = _localizer["RecordNotFound", "Clinic"].Value;
                    _logger.LogInformation(_localizer["LogErrorMessage", "ClinicRepository/Edit", response.ErrorMessage]);
                    return response;
                }

                clinicRecord.Name = value.Name;
                _dbContext.Attach(clinicRecord);
                await _dbContext.SaveChangesAsync();

                response.Result = value;
                response.IsSucceed = true;
                response.SuccessMessage = _localizer["SucceedMessage", _localizer["Edit"]];
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
                var result = await _dbContext.ClinicsDbSet.Where(m => m.Id == Id && m.IsDeleted == false).FirstOrDefaultAsync();

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
                response.IsSucceed = true;
                response.SuccessMessage = _localizer["SucceedMessage", _localizer["Get"]];
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

                var result = await _dbContext.ClinicsDbSet.Where(m => m.IsDeleted == false).ToListAsync();

                response.Result = result.Select(m => new Clinic()
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList();
                response.IsSucceed = true;
                response.SuccessMessage = _localizer["SucceedMessage", _localizer["List"]];
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
                if (_dbContext.ClinicsDbSet.Any(m => m.Id == Id) == false)
                {
                    response.ErrorMessage = _localizer["RecordNotFound", "Clinic"].Value;
                    _logger.LogInformation(_localizer["LogErrorMessage", "ClinicRepository/Delete", response.ErrorMessage]);
                    return response;
                }

                var clinicRecord = _dbContext.ClinicsDbSet.Where(m => m.Id == Id).FirstOrDefault();
                clinicRecord.IsDeleted = true;
                _dbContext.Attach(clinicRecord);

                //Updating Equipmens whichs are related to clinic
                var definedEquipmentsOfClinic = _dbContext.EquipmentsDbSet.Where(m => m.ClinicId == Id).ToList();
                definedEquipmentsOfClinic.ForEach(a => a.IsDeleted = true);
                _dbContext.SaveChanges();

                response.Result = await _dbContext.SaveChangesAsync() > 0;
                response.IsSucceed = true;
                response.SuccessMessage = _localizer["SucceedMessage", _localizer["Delete"]];
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
