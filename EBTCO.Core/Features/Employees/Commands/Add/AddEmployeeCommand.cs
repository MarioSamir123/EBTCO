using EBTCO.Core.Api;
using EBTCO.Domain;
using MediatR;

namespace EBTCO.Core.Features.Employees.Commands.Add
{
    public record AddEmployeeCommand(Guid OfficeId, String FirstName, String LastName, DateOnly Birthday) : IRequest<APIResponse<AddEmployeeCommandResponse>>
    {
        public Employee Map()
        {
            return new Employee
            {
                Name = new Domain.Abstracts.Name { FirstName = FirstName, LastName = LastName },
                Birthday = Birthday,
                OfficeID = OfficeId,
                Timestamp = DateTime.UtcNow,
            };
        }
    }
}
