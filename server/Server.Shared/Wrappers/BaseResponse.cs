namespace Server.Shared.Wrappers;

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
