using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Owners.Queries.GetAll
{
    public record GetAllOwnersQuery : IRequest<APIResponse<GetAllOwnersQueryResponse>>;
}
