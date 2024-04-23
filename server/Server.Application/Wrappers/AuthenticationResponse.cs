using Server.Domain.Entities;

namespace Server.Application.Wrappers
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Token { get; set; } = null!;

        public AuthenticationResponse(string pToken, bool pSuccess, string pMessage) : base(pSuccess, pMessage)
        {
            Token = pToken;
        }
    }
}
