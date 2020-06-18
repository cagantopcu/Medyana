using Medyana.Contract;
using Medyana.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Medyana.Test
{
    public class ClinicRepositoryFake : IClinicRepositoryFake<Clinic>
    {
        private ApiResult<Clinic> _apiResultDummyItem;
        private ApiResult<List<Clinic>> _apiResultDummyList;
        private Clinic _dummyModel;
        private List<Clinic> _listDummyModel;
        private ApiResult<bool> _deleteDummyModel;
        private bool _deleteDummuModel;

        public ApiResult<Clinic> ApiResultDummyItem { get => this._apiResultDummyItem; set => _apiResultDummyItem = value; }
        public ApiResult<List<Clinic>> ApiResultDummyList { get => _apiResultDummyList; set => _apiResultDummyList = value; }
        public Clinic DummyModel { get => _dummyModel; set => _dummyModel = value; }
        public List<Clinic> ListDummyModel { get => _listDummyModel; set => _listDummyModel = value; }
        public ApiResult<bool> ApiResultDeleteDummyModel { get => _deleteDummyModel; set => _deleteDummyModel = value; }
        public bool DeleteDummyModel { get => _deleteDummuModel; set => _deleteDummuModel = value; }

        public async Task<ApiResult<Clinic>> Add(Clinic value)
        {
            ApiResultDummyItem.Result = DummyModel;
            return ApiResultDummyItem;
        }

        public async Task<ApiResult<bool>> Delete(int Id)
        {
            ApiResultDeleteDummyModel.Result = DeleteDummyModel;
            return ApiResultDeleteDummyModel;
        }

        public async Task<ApiResult<Clinic>> Edit(Clinic value)
        {
            ApiResultDummyItem.Result = DummyModel;
            ApiResultDummyItem.Result.Name = value.Name;
            return ApiResultDummyItem;
        }

        public async Task<ApiResult<Clinic>> Get(int Id)
        {
            ApiResultDummyItem.Result = DummyModel;
            return ApiResultDummyItem;
        }

        public async Task<ApiResult<List<Clinic>>> List()
        {
            ApiResultDummyList.Result = ListDummyModel;
            return ApiResultDummyList;
        }
    }
}
