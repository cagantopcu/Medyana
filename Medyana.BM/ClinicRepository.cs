using Medyana.BM.DbObject;
using Medyana.Contract;
using Medyana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.BM
{
    public class ClinicRepository : IClinicRepository<Clinic>
    {
        public ApiResult<Clinic> Add(Clinic value)
        {
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

        public ApiResult<Clinic> Edit(Clinic value)
        {
            ApiResult<Clinic> response = new ApiResult<Clinic>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var clinicRecord = dbContext.ClinicsDbSet.Where(m => m.Id == value.Id).FirstOrDefault();
                    // TO DO Nullcheck
                    clinicRecord.Name = value.Name;
                    dbContext.Attach(clinicRecord);
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

        public ApiResult<Clinic> Get(int Id)
        {
            ApiResult<Clinic> response = new ApiResult<Clinic>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var result = dbContext.ClinicsDbSet.Where(m => m.Id == Id).FirstOrDefault();

                    //// TODO mapper
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
                response.ErrorMessage = ex.InnerException.ToString();
            }
            return response;
        }

        public ApiResult<List<Clinic>> List()
        {
            ApiResult<List<Clinic>> response = new ApiResult<List<Clinic>>();

            try
            {
                using (var dbContext = new MedyanaDbContext())
                {
                    var result = dbContext.ClinicsDbSet.ToList();

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
                response.ErrorMessage = ex.InnerException.ToString();
            }

            return response;

        }
    }
}
