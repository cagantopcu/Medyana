using Medyana.BM.DbObject;
using Medyana.Contract;
using Medyana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.BM
{
    public class EquipmentRepository : IEquipmentRepository<Equipment>
    {
        public ApiResult<Equipment> Add(Equipment value)
        {
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
                response.ErrorMessage = ex.InnerException.ToString();
            }
            return response;
        }

        public ApiResult<Equipment> Edit(Equipment value)
        {
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
                response.ErrorMessage = ex.InnerException.ToString();
            }
            return response;

        }

        public ApiResult<Equipment> Get(int Id)
        {
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
                response.ErrorMessage = ex.InnerException.ToString();
            }
            return response;
        }

        public ApiResult<List<Equipment>> List()
        {
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
                response.ErrorMessage = ex.InnerException.ToString();
            }

            return response;

        }
    }
}
