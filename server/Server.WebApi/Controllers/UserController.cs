using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Features.User.Commands;
using Server.Application.Features.User.Queries;
using Server.Application.Handlers.User.Commands;
using Server.Application.Wrappers;

namespace Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthenticationResponse>> Create(CreateUserCommand user)
        {
            return Ok(await _mediator.Send(user));
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<AuthenticationResponse>> Update(UpdateUserCommand user)
        {
            return Ok(await _mediator.Send(user));
        }

        [HttpPost("list")]
        [Authorize]
        public async Task<ActionResult<BaseDataResponse<List<UserDto>>>> List(ListUsersQuery command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(LoginUserQuery user)
        {
            return Ok(await _mediator.Send(user));
        }

        [HttpPost("detail")]
        public async Task<ActionResult<AuthenticationResponse>> Detail(DetailUserQuery user)
        {
            return Ok(await _mediator.Send(user));
        }

        [HttpPost("uploadImage")]
        public ActionResult<BaseDataResponse<string>> UploadImage(IFormFile image)
        {
            string directory = Path.Combine("wwwroot", "images", "users");
            string fileName = image.FileName.Replace(" ", "");
            string imageName = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + "-" + fileName;

            try
            {
                var filePath = Path.Combine(directory, imageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                return new BaseDataResponse<string>(imageName, true, ResponseMessages.Success);
            }
            catch (Exception ex)
            {
                return new BaseDataResponse<string>(ex.Message, false, ResponseMessages.AnErrorOccurredWhileLoadingTheImage);
            }
        }

    }
}
