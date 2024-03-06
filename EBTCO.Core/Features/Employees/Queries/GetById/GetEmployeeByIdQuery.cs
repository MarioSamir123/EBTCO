using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Employees.Queries.GetById
{
    public record GetEmployeeByIdQuery(Guid Id) : IRequest<APIResponse<GetEmployeeByIdQueryResponse>>;
}
