using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medyana.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string Deserialize(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
