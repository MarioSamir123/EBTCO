using EBTCO.Core.Api;
using MediatR;

namespace EBTCO.Core.Features.Owners.Queries.GetById
{
    public record GetOwnerByIdQuery(Guid Id) : IRequest<APIResponse<GetOwnerByIdQueryResponse>>;
}
