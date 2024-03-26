using EBTCO.Core.Api;
using EBTCO.Core.Features.Identity.Commands.Login;
using EBTCO.Core.Features.Identity.Commands.LoginByGoogle;
using EBTCO.Core.Features.Identity.Commands.Signup;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EBTCO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<APIResponse<LoginCommandResponse>>> Sale([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpPost("Signup")]
        public async Task<ActionResult<APIResponse<SignupCommandResponse>>> Signup([FromBody] SignupCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
        [HttpPost("LoginByGoogle")]
        public async Task<ActionResult<APIResponse<LoginByGoogleCommandResponse>>> LoginByGoogle([FromBody] LoginByGoogleCommand command)
        {
            var result = await _mediator.Send(command);
            return GetApiResponse(result);
        }
    }
}
