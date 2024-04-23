using Server.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Dtos
{
    public class UserDetailDto : UserDto
    {
        public string Email { get; set; }
    }
}
