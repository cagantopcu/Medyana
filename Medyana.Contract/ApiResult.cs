using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medyana.Contract
{
    public class ApiResult<TResult>
    {
        public TResult Result { get; set; }
        public bool IsSucceed { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}
