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
    public class EquipmentRepository : IEquipmentRepository<Equipment>
    {

        private readonly ILogger<EquipmentRepository> _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EquipmentRepository(ILogger<EquipmentRepository> logger, IStringLocalizer<SharedResources> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            _logger.LogInformation(_localizer["LogClassConstructor", "EquipmentRepository"]);
        }

        /// <summary>
        /// Adds New Equipment
        /// </summary>
        /// <param name="value">Equipment Item</param>
        /// <returns>Equipment Item</returns>
        public async Task<ApiResult<Equipment>> Add(Equipment value)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "EquipmentRepository/Add"]);
            ApiResult<Equipment> response = new ApiResult<Equipment>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    if (dbContext.ClinicsDbSet.Any(m => m.Id == value.ClinicId) == false)
                    {
                        response.ErrorMessage = _localizer["RecordNotFound", "Clinic"].Value;
                        _logger.LogInformation(_localizer["LogErrorMessage", "EquipmentRepository/Delete", response.ErrorMessage]);
                        return response;
                    }

                    EquipmentDbObject equipmentDbObject = new EquipmentDbObject()
                    {
                        Name = value.Name,
                        ClinicId = value.ClinicId,
                        Quantity = value.Quantity,
                        SupplyDate = value.SupplyDate,
                        UnitPrice = value.UnitPrice,
                        UsageRate = value.UsageRate
                    };


                    dbContext.EquipmentsDbSet.Add(equipmentDbObject);
                    await dbContext.SaveChangesAsync();

                    value.Id = equipmentDbObject.Id;
                }

                response.Result = value;
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(_localizer["LogMethodError", "EquipmentRepository/Add", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(_localizer["LogMethodSucceed", "EquipmentRepository/Add", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "EquipmentRepository/Add", response.Deserialize()]);
            return response;
        }

        /// <summary>
        /// Edits Equipment
        /// </summary>
        /// <param name="value">Current Equipment Item</param>
        /// <returns>Updated Equipment Item</returns>
        public async Task<ApiResult<Equipment>> Edit(Equipment value)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "EquipmentRepository/Edit"]);
            ApiResult<Equipment> response = new ApiResult<Equipment>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var equipmentRecord = dbContext.EquipmentsDbSet.Where(m => m.Id == value.Id).FirstOrDefault();

                    if (equipmentRecord == null)
                    {
                        response.ErrorMessage = _localizer["RecordNotFound", "Equipment"].Value;
                        _logger.LogInformation(_localizer["LogErrorMessage", "EquipmentRepository/Edit", response.ErrorMessage]);
                        return response;
                    }

                    equipmentRecord.Name = value.Name;
                    equipmentRecord.ClinicId = value.ClinicId;
                    equipmentRecord.Quantity = value.Quantity;
                    equipmentRecord.SupplyDate = value.SupplyDate;
                    equipmentRecord.UnitPrice = value.UnitPrice;
                    equipmentRecord.UsageRate = value.UsageRate;

                    dbContext.Attach(equipmentRecord);
                    await dbContext.SaveChangesAsync();
                }

                response.Result = value;
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(_localizer["LogMethodError", "EquipmentRepository/Edit", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(_localizer["LogMethodSucceed", "EquipmentRepository/Edit", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "EquipmentRepository/Edit", response.Deserialize()]);

            return response;
        }

        /// <summary>
        /// Gets Equipment Item
        /// </summary>
        /// <param name="Id">Unique Identifier Of Equipment</param>
        /// <returns>Matched Equipment Item</returns>
        public async Task<ApiResult<Equipment>> Get(int Id)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "EquipmentRepository/Get"]);

            ApiResult<Equipment> response = new ApiResult<Equipment>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var result = await dbContext.EquipmentsDbSet.Where(m => m.Id == Id).FirstOrDefaultAsync();

                    if (result == null)
                    {
                        response.ErrorMessage = _localizer["RecordNotFound", "Equipment"].Value;
                        _logger.LogInformation(_localizer["LogErrorMessage", "EquipmentRepository/Get", response.ErrorMessage]);
                        return response;
                    }

                    response.Result = new Equipment()
                    {
                        Id = result.Id,
                        Name = result.Name,
                        ClinicId = result.ClinicId,
                        Quantity = result.Quantity,
                        SupplyDate = result.SupplyDate,
                        UnitPrice = result.UnitPrice,
                        UsageRate = result.UsageRate

                    };
                }
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(_localizer["LogMethodError", "EquipmentRepository/Get", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }
            _logger.LogInformation(_localizer["LogMethodSucceed", "EquipmentRepository/Get", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "EquipmentRepository/Get", response.Deserialize()]);
            return response;
        }

        /// <summary>
        /// Lists All Equipment
        /// </summary>
        /// <returns>Defined All Clinis</returns>
        public async Task<ApiResult<List<Equipment>>> List()
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "EquipmentRepository/List"]);

            ApiResult<List<Equipment>> response = new ApiResult<List<Equipment>>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var result = await dbContext.EquipmentsDbSet.ToListAsync();

                    response.Result = result.Select(m => new Equipment()
                    {
                        Id = m.Id,
                        Name = m.Name,
                        ClinicId = m.ClinicId,
                        Quantity = m.Quantity,
                        SupplyDate = m.SupplyDate,
                        UnitPrice = m.UnitPrice,
                        UsageRate = m.UsageRate
                    }).ToList();
                }
                response.IsSucceed = true;

            }
            catch (Exception ex)
            {
                _logger.LogInformation(_localizer["LogMethodError", "EquipmentRepository/List", ex.InnerException.ToString()]);
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(_localizer["LogMethodSucceed", "EquipmentRepository/List", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "EquipmentRepository/List", response.Deserialize()]);

            return response;
        }


        /// <summary>
        /// Deletes Record By Passed Id 
        /// </summary>
        /// <param name="Id">Unique Identifier Of Record</param>
        /// <returns>Is Deleted</returns>
        public async Task<ApiResult<bool>> Delete(int Id)
        {
            _logger.LogInformation(_localizer["LogMethodCalled", "EquipmentRepository/Delete"]);

            ApiResult<bool> response = new ApiResult<bool>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    if (dbContext.EquipmentsDbSet.Any(m => m.Id == Id) == false)
                    {
                        response.ErrorMessage = _localizer["RecordNotFound", "Equipment" ].Value;                        
                        _logger.LogInformation(_localizer["LogErrorMessage", "EquipmentRepository/Delete", response.ErrorMessage]);
                        return response;
                    }

                    EquipmentDbObject record = new EquipmentDbObject() { Id = Id };

                    dbContext.Attach(record);
                    dbContext.Remove(record);

                    response.Result = await dbContext.SaveChangesAsync() > 0;
                    response.IsSucceed = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(_localizer["LogMethodError", "EquipmentRepository/Delete", ex.InnerException.ToString()]);

                response.ErrorMessage = ex.InnerException.ToString();
            }
            _logger.LogInformation(_localizer["LogMethodSucceed", "EquipmentRepository/Delete", response.IsSucceed.Deserialize()]);
            _logger.LogInformation(_localizer["LogMethodResult", "EquipmentRepository/Delete", response.Deserialize()]);
            return response;
        }
    }
}
