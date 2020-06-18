using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.Contract
{
    public interface IEquipmentRepository<TEquipment> : IRepository
    {
        Task<ApiResult<TEquipment>> Get(int Id);
        Task<ApiResult<List<TEquipment>>> List();
        Task<ApiResult<TEquipment>> Add(TEquipment value);
        Task<ApiResult<TEquipment>> Edit(TEquipment value);
        Task<ApiResult<bool>> Delete(int Id);
    }
}
