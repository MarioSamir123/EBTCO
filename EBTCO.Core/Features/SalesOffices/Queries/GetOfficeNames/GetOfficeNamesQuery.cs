using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.SalesOffices.Queries.GetOfficeNames
{
    public record GetOfficeNamesQuery : IRequest<APIResponse<GetOfficeNamesQueryResponse>>;
}
