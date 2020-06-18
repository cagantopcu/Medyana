using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.Contract
{
    public interface IClinicRepository<TClinic> : IRepository
    {
        Task<ApiResult<TClinic>> Get(int Id);
        Task<ApiResult<List<TClinic>>> List();
        Task<ApiResult<TClinic>> Add(TClinic value);
        Task<ApiResult<TClinic>> Edit(TClinic value);
        Task<ApiResult<bool>> Delete(int Id);
    }
}
