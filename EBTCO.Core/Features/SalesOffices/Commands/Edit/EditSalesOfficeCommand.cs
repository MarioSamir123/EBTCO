using EBTCO.Core.Api;
using EBTCO.Core.Features.SalesOffices.DTO;
using MediatR;

namespace EBTCO.Core.Features.SalesOffices.Commands.Edit
{
    public record EditSalesOfficeCommand(Guid Id, String officeName, AddressDto Address) : IRequest<APIResponse<EditSalesOfficeCommandResponse>>;
}
