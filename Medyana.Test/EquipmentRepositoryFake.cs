using Medyana.Contract;
using Medyana.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medyana.Test
{
    public class EquipmentRepositoryFake : IEquipmentRepositoryFake<Equipment>
    {
        private ApiResult<Equipment> _apiResultDummyItem;
        private ApiResult<List<Equipment>> _apiResultDummyList;
        private Equipment _dummyModel;
        private List<Equipment> _listDummyModel;
        private ApiResult<bool> _deleteDummyModel;
        private bool _deleteDummuModel;

        public ApiResult<Equipment> ApiResultDummyItem { get => this._apiResultDummyItem; set => _apiResultDummyItem = value; }
        public ApiResult<List<Equipment>> ApiResultDummyList { get => _apiResultDummyList; set => _apiResultDummyList = value; }
        public Equipment DummyModel { get => _dummyModel; set => _dummyModel = value; }
        public List<Equipment> ListDummyModel { get => _listDummyModel; set => _listDummyModel = value; }
        public ApiResult<bool> ApiResultDeleteDummyModel { get => _deleteDummyModel; set => _deleteDummyModel = value; }
        public bool DeleteDummyModel { get => _deleteDummuModel; set => _deleteDummuModel = value; }

        public ApiResult<Equipment> Add(Equipment value)
        {
            ApiResultDummyItem.Result = DummyModel;
            return ApiResultDummyItem;
        }

        public ApiResult<bool> Delete(int Id)
        {
            ApiResultDeleteDummyModel.Result = DeleteDummyModel;
            return ApiResultDeleteDummyModel;
        }

        public ApiResult<Equipment> Edit(Equipment value)
        {
            ApiResultDummyItem.Result = DummyModel;
            ApiResultDummyItem.Result.Name = value.Name;
            return ApiResultDummyItem;
        }

        public ApiResult<Equipment> Get(int Id)
        {
            ApiResultDummyItem.Result = DummyModel;
            return ApiResultDummyItem;
        }

        public ApiResult<List<Equipment>> List()
        {
            ApiResultDummyList.Result = ListDummyModel;
            return ApiResultDummyList;
        }
    }
}
