using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Employees.Commands.Delete
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, APIResponse<DeleteEmployeeCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<DeleteEmployeeCommandResponse>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeRepo = _unitOfWork.GetRepository<Employee>();
            var employee = await employeeRepo
                .GetSource()
                .FirstOrDefaultAsync(row => !row.IsDeleted && row.ID.Equals(request.Id));

            if (employee == null)
            {
                return new APIResponse<DeleteEmployeeCommandResponse>
                {
                    Errors = new List<string> { "This Employee is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            employee.IsDeleted = true;
            employee.Timestamp = DateTime.UtcNow;
            employeeRepo.Update(employee);
            await _unitOfWork.SaveChangesAsync();
            return new APIResponse<DeleteEmployeeCommandResponse>
            {
                Data = new DeleteEmployeeCommandResponse("Employee deleted successfully!"),
                HttpStatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
