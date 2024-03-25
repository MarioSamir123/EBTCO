using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Identity.Commands.Login
{
    public record LoginCommand(String email, String password) : IRequest<APIResponse<LoginCommandResponse>>;
}
