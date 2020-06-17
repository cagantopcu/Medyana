using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.Contract
{
    public interface IEquipmentRepository<TEquipment> : IRepository
    {
        ApiResult<TEquipment> Get(int Id);
        ApiResult<List<TEquipment>> List();
        ApiResult<TEquipment> Add(TEquipment value);
        ApiResult<TEquipment> Edit(TEquipment value);
    }
}
