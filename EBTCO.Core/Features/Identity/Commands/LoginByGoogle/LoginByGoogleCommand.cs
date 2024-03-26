using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Identity.Commands.LoginByGoogle
{
    public record LoginByGoogleCommand(String googleToken) : IRequest<APIResponse<LoginByGoogleCommandResponse>>;
}
