using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.SalesOffices.Commands.Delete
{
    public class DeleteSalesOfficeCommandHandler : IRequestHandler<DeleteSalesOfficeCommand, APIResponse<DeleteSalesOfficeCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSalesOfficeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<DeleteSalesOfficeCommandResponse>> Handle(DeleteSalesOfficeCommand request, CancellationToken cancellationToken)
        {
            var salesOfficeRepo = _unitOfWork.GetRepository<SalesOffice>();
            var salesOffice = await salesOfficeRepo
                .GetSource()
                .AsNoTracking()
                .FirstOrDefaultAsync(row => row.ID.Equals(request.Id));
            if (salesOffice == null)
            {
                return new APIResponse<DeleteSalesOfficeCommandResponse>
                {
                    Errors = new List<string> { "This office is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                };
            }

            salesOffice.IsDeleted = true;
            salesOffice.Timestamp = DateTime.UtcNow;

            salesOfficeRepo.Update(salesOffice);
            await _unitOfWork.SaveChangesAsync();

            return new APIResponse<DeleteSalesOfficeCommandResponse>
            {
                Data = new DeleteSalesOfficeCommandResponse("Office deleted successfully!"),
                HttpStatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
