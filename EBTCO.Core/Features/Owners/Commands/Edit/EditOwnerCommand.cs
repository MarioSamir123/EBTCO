using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Owners.Commands.Edit
{
    public record EditOwnerCommand(Guid Id, String FirstName, String LastName) : IRequest<APIResponse<EditOwnerCommandResponse>>;
}
