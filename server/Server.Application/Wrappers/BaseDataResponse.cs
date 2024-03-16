using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Wrappers
{
    public class BaseDataResponse<T> : BaseResponse
    {
        public T Body { get; set; }

        public BaseDataResponse(T pBody, bool pSuccess, string pMessage) : base(pSuccess, pMessage)
        {
            Body = pBody;
        }
    }
}
