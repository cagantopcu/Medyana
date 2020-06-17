using Medyana.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medyana.Test
{
    public interface IEquipmentRepositoryFake<TModel> : IEquipmentRepository<TModel>
    {
        ApiResult<TModel> ApiResultDummyItem { get; set; }
        ApiResult<List<TModel>> ApiResultDummyList { get; set; }
        TModel DummyModel { get; set; }
        List<TModel> ListDummyModel { get; set; }
        ApiResult<bool> ApiResultDeleteDummyModel { get; set; }
        bool DeleteDummyModel { get; set; }

    }
}
