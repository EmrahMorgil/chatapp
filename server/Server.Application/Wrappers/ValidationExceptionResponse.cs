using Server.Application.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Wrappers
{
    public class ValidationExceptionResponse : BaseResponse
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public ValidationExceptionResponse(IDictionary<string, string[]> errors) : base(false, ResponseMessages.ModelValidationFail)
        {
            Errors = errors;
        }
    }
}
