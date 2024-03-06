using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.SalesOffices.Commands.HireManager
{
    public record HireManagerCommand(Guid employeeID, Guid officeID) : IRequest<APIResponse<HireManagerCommandResponse>>;
}
