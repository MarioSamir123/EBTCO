using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Owners.Commands.Delete
{
    public record DeleteOwnerCommand(Guid ownerId) : IRequest<APIResponse<DeleteOwnerCommandResponse>>;
}
