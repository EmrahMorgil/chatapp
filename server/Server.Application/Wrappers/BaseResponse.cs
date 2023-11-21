using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Wrappers
{
    public class BaseResponse<T>
    {
        public T body { get; set; }
        public bool success { get; set; }
        public string token { get; set; }
    }

}
