using Server.Shared.Consts;

namespace Server.Shared.Wrappers;

public class ValidationExceptionResponse : BaseResponse
{
    public IDictionary<string, string[]> Errors { get; set; }
    public ValidationExceptionResponse(IDictionary<string, string[]> errors) : base(false, ResponseMessages.ModelValidationFail)
    {
        Errors = errors;
    }
}
