using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Employees.Queries.GetById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, APIResponse<GetEmployeeByIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<GetEmployeeByIdQueryResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.GetRepository<Employee>()
                .GetSource()
                .AsNoTracking()
                .Where(row => !row.IsDeleted && row.ID.Equals(request.Id))
                .Select(row => new GetEmployeeByIdQueryResponse(row.ID, row.OfficeID, row.Name.FirstName, row.Name.LastName, row.Birthday))
                .FirstOrDefaultAsync();
            if (employee == null)
            {
                return new APIResponse<GetEmployeeByIdQueryResponse>
                {
                    Errors = new List<string> { "This Employee is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                };
            }
            return new APIResponse<GetEmployeeByIdQueryResponse>
            {
                Data = employee,
                HttpStatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
