using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.SalesOffices.Queries.GetById
{
    public record GetSalseOfficeByIdQuery(Guid Id) : IRequest<APIResponse<GetSalseOfficeByIdQueryResponse>>;
}
