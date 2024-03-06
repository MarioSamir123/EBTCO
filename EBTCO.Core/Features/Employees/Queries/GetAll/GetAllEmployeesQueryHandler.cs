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
                .Where(row => !row.IsDeleted)
                .Select(row => new EmployeesDto(row.ID, row.OfficeID, row.Name.FirstName, row.Name.LastName, row.Birthday))
                .ToListAsync();

            return new APIResponse<GetAllEmployeesQueryResponse>
            {
                Data = new GetAllEmployeesQueryResponse(employees),
                HttpStatusCode = employees.Count == 0 ? System.Net.HttpStatusCode.NoContent : System.Net.HttpStatusCode.OK
            };
        }
    }
}
