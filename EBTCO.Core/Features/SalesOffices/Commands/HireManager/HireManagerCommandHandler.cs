using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EBTCO.Core.Features.SalesOffices.Commands.HireManager
{
    public class HireManagerCommandHandler : IRequestHandler<HireManagerCommand, APIResponse<HireManagerCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public HireManagerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<HireManagerCommandResponse>> Handle(HireManagerCommand request, CancellationToken cancellationToken)
        {
            var employeeFullName = await _unitOfWork.GetRepository<Employee>()
                .GetSource()
                .AsNoTracking()
                .Where(row => row.OfficeID.Equals(request.officeID) && row.ID.Equals(request.employeeID))
                .Select(row => $"{row.Name.FirstName} {row.Name.LastName}")
                .FirstOrDefaultAsync();

            if (employeeFullName.IsNullOrEmpty())
            {
                return new APIResponse<HireManagerCommandResponse>
                {
                    Errors = new List<string> { "This Employee is not found in this office!" },
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }

            var salesOfficeQuery = _unitOfWork.GetRepository<SalesOffice>()
                .GetSource()
                .AsNoTracking()
                .Where(row => row.ID.Equals(request.officeID));
            await salesOfficeQuery.ExecuteUpdateAsync(row => row.SetProperty(so => so.ManagerEmpID, so => request.officeID));
            await salesOfficeQuery.ExecuteUpdateAsync(row => row.SetProperty(so => so.ManagerName, so => employeeFullName));

            return new APIResponse<HireManagerCommandResponse>
            {
                Data = new HireManagerCommandResponse("Done!"),
                HttpStatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
