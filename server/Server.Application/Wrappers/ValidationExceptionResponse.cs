using Server.Application.Consts;

namespace Server.Application.Wrappers;

public class ValidationExceptionResponse : BaseResponse
{
    public IDictionary<string, string[]> Errors { get; set; }
    public ValidationExceptionResponse(IDictionary<string, string[]> errors) : base(false, ResponseMessages.ModelValidationFail)
    {
        Errors = errors;
    }
}
