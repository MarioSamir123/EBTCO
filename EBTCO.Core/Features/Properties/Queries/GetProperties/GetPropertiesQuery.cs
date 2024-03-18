using EBTCO.Core.Api;
using EBTCO.Domain;
using MediatR;
using System.Linq.Expressions;

namespace EBTCO.Core.Features.Properties.Queries.GetProperties
{
    public record GetPropertiesQuery(Guid Id) : IRequest<APIResponse<GetPropertiesQueryResponse>>
    {
        public Expression<Func<Property, bool>> GetFilter() => row => !row.IsDeleted && row.OfficeID.Equals(Id);
    }
}
