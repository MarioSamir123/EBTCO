using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Employees.Commands.Delete
{
    public record DeleteEmployeeCommand(Guid Id) : IRequest<APIResponse<DeleteEmployeeCommandResponse>>
    {
    }
}
