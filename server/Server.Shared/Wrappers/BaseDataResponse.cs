namespace Server.Shared.Wrappers;

public class BaseDataResponse<T> : BaseResponse
{
    public T Body { get; set; }

    public BaseDataResponse(T pBody, bool pSuccess, string pMessage) : base(pSuccess, pMessage)
    {
        Body = pBody;
    }
}
