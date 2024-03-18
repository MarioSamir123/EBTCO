using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Employees.Queries.GetAll
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, APIResponse<GetAllEmployeesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<GetAllEmployeesQueryResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.GetRepository<Employee>()
                .GetSource()
                .AsNoTracking()
                .Include(row => row.SalesOffice)
                .Where(request.GetFilter())
                .Select(row => new EmployeesDto(row.ID, row.OfficeID, row.Name.FirstName, row.Name.LastName, row.SalesOffice.OfficeName, row.Birthday))
                .ToListAsync();

            return new APIResponse<GetAllEmployeesQueryResponse>
            {
                Data = new GetAllEmployeesQueryResponse(employees),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
