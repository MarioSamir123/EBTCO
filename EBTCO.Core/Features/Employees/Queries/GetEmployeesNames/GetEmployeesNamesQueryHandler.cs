using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Employees.Queries.GetEmployeesNames
{
    public class GetEmployeesNamesQueryHandler : IRequestHandler<GetEmployeesNamesQuery, APIResponse<GetEmployeesNamesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeesNamesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<GetEmployeesNamesQueryResponse>> Handle(GetEmployeesNamesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.GetRepository<Employee>()
                .GetSource()
                .AsNoTracking()
                .Where(request.GetFilter())
                .ToDictionaryAsync(row => row.ID.ToString(), row => $"{row.Name.FirstName} {row.Name.LastName}");
            return new APIResponse<GetEmployeesNamesQueryResponse>
            {
                Data = new GetEmployeesNamesQueryResponse(employees),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
