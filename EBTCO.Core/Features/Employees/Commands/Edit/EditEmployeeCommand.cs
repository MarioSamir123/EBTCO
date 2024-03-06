using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Employees.Commands.Edit
{
    public record EditEmployeeCommand(Guid Id, Guid OfficeId, String FirstName, String LastName, DateTime Birthday)
        : IRequest<APIResponse<EditEmployeeCommandResponse>>;
}
