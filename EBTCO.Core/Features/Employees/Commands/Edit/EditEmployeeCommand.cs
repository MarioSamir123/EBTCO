using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Employees.Commands.Edit
{
    public record EditEmployeeCommand(Guid Id, String FirstName, String LastName, DateOnly Birthday)
        : IRequest<APIResponse<EditEmployeeCommandResponse>>;
}
