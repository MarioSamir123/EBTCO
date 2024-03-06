using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.SalesOffices.Commands.Delete
{
    public record DeleteSalesOfficeCommand(Guid Id) : IRequest<APIResponse<DeleteSalesOfficeCommandResponse>>;
}
