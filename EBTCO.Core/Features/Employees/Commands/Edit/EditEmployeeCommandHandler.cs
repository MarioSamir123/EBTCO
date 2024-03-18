using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Employees.Commands.Edit
{
    public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand, APIResponse<EditEmployeeCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<EditEmployeeCommandResponse>> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            var employee = await employeeRepo
                .GetSource()
                .FirstOrDefaultAsync(row => !row.IsDeleted && row.ID.Equals(request.Id));

            if (employee == null)
            {
                return new APIResponse<EditEmployeeCommandResponse>
                {
                    Errors = new List<string> { "This Employee is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            employee.Name.FirstName = request.FirstName;
            employee.Name.LastName = request.LastName;
            employee.Birthday = request.Birthday;
            employee.Timestamp = DateTime.UtcNow;
            employeeRepo.Update(employee);
            await _unitOfWork.SaveChangesAsync();
            return new APIResponse<EditEmployeeCommandResponse>
            {
                Data = new EditEmployeeCommandResponse(employee.ID),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
