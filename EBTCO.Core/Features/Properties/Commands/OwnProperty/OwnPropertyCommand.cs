using EBTCO.Core.Api;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EBTCO.Core.Features.Properties.Commands.OwnProperty
{
    public record OwnPropertyCommand(Guid ownerId, Guid propId, [Range(0, 100)]int percentage) : IRequest<APIResponse<OwnPropertyCommandResponse>>;
}
