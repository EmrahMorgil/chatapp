using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Wrappers
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public BaseResponse(bool pSuccess, string pMessage)
        {
            Success = pSuccess;
            Message = pMessage;
        }

    }

}
