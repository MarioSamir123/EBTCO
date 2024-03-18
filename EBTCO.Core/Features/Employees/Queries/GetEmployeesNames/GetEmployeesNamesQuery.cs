using EBTCO.Core.Api;
using EBTCO.Domain;
using MediatR;
using System.Linq.Expressions;

namespace EBTCO.Core.Features.Employees.Queries.GetEmployeesNames
{
    public record GetEmployeesNamesQuery(Guid officeId) : IRequest<APIResponse<GetEmployeesNamesQueryResponse>>
    {
        public Expression<Func<Employee, bool>> GetFilter()
        {
            return row => row.OfficeID.Equals(officeId);
        }
    }
}
