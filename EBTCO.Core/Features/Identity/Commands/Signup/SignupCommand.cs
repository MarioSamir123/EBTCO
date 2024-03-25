using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Identity.Commands.Signup
{
    public record SignupCommand(String email, String password) : IRequest<APIResponse<SignupCommandResponse>>;
}
