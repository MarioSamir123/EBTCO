using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Employees.Queries.GetAll
{
    public record GetAllEmployeesQuery : IRequest<APIResponse<GetAllEmployeesQueryResponse>>;
}
