using Medyana.BM.DbObject;
using Medyana.Contract;
using Medyana.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.BM
{
    public class EquipmentRepository : IEquipmentRepository<Equipment>
    {

        private readonly ILogger<EquipmentRepository> _logger;

        public EquipmentRepository(ILogger<EquipmentRepository> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adds New Equipment
        /// </summary>
        /// <param name="value">Equipment Item</param>
        /// <returns>Equipment Item</returns>
        public ApiResult<Equipment> Add(Equipment value)
        {

            _logger.LogInformation("Method Called - EquipmentRepository/Add");
            ApiResult<Equipment> response = new ApiResult<Equipment>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
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
                    dbContext.SaveChanges();
                }

                response.Result = value;
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("EquipmentRepository/Add - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("EquipmentRepository/Add - IsSucceed:  {0}", response.IsSucceed));
            return response;
        }

        /// <summary>
        /// Edits Equipment
        /// </summary>
        /// <param name="value">Current Equipment Item</param>
        /// <returns>Updated Equipment Item</returns>
        public ApiResult<Equipment> Edit(Equipment value)
        {
            _logger.LogInformation("Method Called - EquipmentRepository/Edit");
            ApiResult<Equipment> response = new ApiResult<Equipment>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var equipmentRecord = dbContext.EquipmentsDbSet.Where(m => m.Id == value.Id).FirstOrDefault();

                    // TO DO Nullcheck
                    equipmentRecord.Name = value.Name;
                    equipmentRecord.ClinicId = value.ClinicId;
                    equipmentRecord.Quantity = value.Quantity;
                    equipmentRecord.SupplyDate = value.SupplyDate;
                    equipmentRecord.UnitPrice = value.UnitPrice;
                    equipmentRecord.UsageRate = value.UsageRate;

                    dbContext.Attach(equipmentRecord);
                    dbContext.SaveChanges();
                }

                response.Result = value;
                response.IsSucceed = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("EquipmentRepository/Edit - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("EquipmentRepository/Edit - IsSucceed: {0}", response.IsSucceed));
            return response;

        }

        /// <summary>
        /// Gets Equipment Item
        /// </summary>
        /// <param name="Id">Unique Identifier Of Equipment</param>
        /// <returns>Matched Equipment Item</returns>
        public ApiResult<Equipment> Get(int Id)
        {
            _logger.LogInformation("Method Called - EquipmentRepository/Get");

            ApiResult<Equipment> response = new ApiResult<Equipment>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var result = dbContext.EquipmentsDbSet.Where(m => m.Id == Id).FirstOrDefault();

                    //// TODO mapper
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
                _logger.LogError(string.Format("EquipmentRepository/Get - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }
            _logger.LogInformation(string.Format("EquipmentRepository/Get - IsSucceed:  {0}", response.IsSucceed));
            return response;
        }

        /// <summary>
        /// Lists All Equipment
        /// </summary>
        /// <returns>Defined All Clinis</returns>
        public ApiResult<List<Equipment>> List()
        {
            _logger.LogInformation("Method Called - EquipmentRepository/List");
            ApiResult<List<Equipment>> response = new ApiResult<List<Equipment>>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var result = dbContext.EquipmentsDbSet.ToList();

                    //// TODO mapper
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
                _logger.LogError(string.Format("EquipmentRepository/List - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("EquipmentRepository/List - IsSucceed: {0}", response.IsSucceed));
            return response;

        }


        /// <summary>
        /// Deletes Record By Passed Id 
        /// </summary>
        /// <param name="Id">Unique Identifier Of Record</param>
        /// <returns>Is Deleted</returns>
        public ApiResult<bool> Delete(int Id)
        {
            _logger.LogInformation("Method Called - EquipmentRepository/Delete");

            ApiResult<bool> response = new ApiResult<bool>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    ClinicDbObject record = new ClinicDbObject() { Id = Id };
                    dbContext.Attach(record);
                    dbContext.Remove(record);

                    response.Result = dbContext.SaveChanges() > 0;
                    response.IsSucceed = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("EquipmentRepository/Delete - {0}", ex.InnerException.ToString()));
                response.ErrorMessage = ex.InnerException.ToString();
            }

            _logger.LogInformation(string.Format("EquipmentRepository/Delete - IsSucceed: {0}", response.IsSucceed));
            return response;
        }
    }
}
