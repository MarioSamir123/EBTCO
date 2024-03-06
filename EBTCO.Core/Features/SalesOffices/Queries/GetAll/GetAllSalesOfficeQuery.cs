using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.SalesOffices.Queries.GetAll
{
    public record GetAllSalesOfficeQuery : IRequest<APIResponse<GetAllSalesOfficeQueryResponse>>;
}
