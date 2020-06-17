using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.Contract
{
    public interface IClinicRepository<TClinic> : IRepository
    {
        ApiResult<TClinic> Get(int Id);
        ApiResult<List<TClinic>> List();
        ApiResult<TClinic> Add(TClinic value);
        ApiResult<TClinic> Edit(TClinic value);
        ApiResult<bool> Delete(int Id);
    }
}
