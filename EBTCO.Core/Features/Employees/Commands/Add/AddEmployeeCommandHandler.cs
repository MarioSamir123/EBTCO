using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Employees.Commands.Add
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, APIResponse<AddEmployeeCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<AddEmployeeCommandResponse>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var officeExist = await _unitOfWork.GetRepository<SalesOffice>()
                .GetSource()
                .AsNoTracking()
                .AnyAsync(row => !row.IsDeleted && row.ID.Equals(request.OfficeId));

            if (!officeExist)
            {
                return new APIResponse<AddEmployeeCommandResponse>
                {
                    Errors = new List<string> { "This Office is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            var employee = request.Map();
            await _unitOfWork.GetRepository<Employee>().AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            return new APIResponse<AddEmployeeCommandResponse>
            {
                Data = new AddEmployeeCommandResponse(employee.ID),
                HttpStatusCode = System.Net.HttpStatusCode.Created,
            };
        }
    }
}
