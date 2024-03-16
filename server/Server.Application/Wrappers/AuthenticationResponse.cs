using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Wrappers
{
    public class AuthenticationResponse : BaseDataResponse<User>
    {
        public string Token { get; set; } = null!;

        public AuthenticationResponse(User pBody, bool pSuccess, string pToken, string pMessage) : base(pBody, pSuccess, pMessage)
        {
            Token = pToken;
        }
    }
}
