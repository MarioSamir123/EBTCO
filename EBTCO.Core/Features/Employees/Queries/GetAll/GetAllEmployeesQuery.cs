using EBTCO.Core.Api;
using EBTCO.Domain;
using MediatR;
using System.Linq.Expressions;

namespace EBTCO.Core.Features.Employees.Queries.GetAll
{
    public record GetAllEmployeesQuery(Guid? officeId) : IRequest<APIResponse<GetAllEmployeesQueryResponse>> 
    { 
        public Expression<Func<Employee, bool>> GetFilter()
        {
            if (officeId == null || officeId.Equals(Guid.Empty))
            {
                return row => !row.IsDeleted;
            }
            return row => !row.IsDeleted && row.OfficeID.Equals(officeId);
        }
    }
}
