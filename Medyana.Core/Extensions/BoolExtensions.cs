using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medyana.Core.Extensions
{
    public static class BoolExtensions
    {
        public static string Deserialize(this bool value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
