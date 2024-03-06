using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.SalesOffices.Commands.Edit
{
    public class EditSalesOfficeCommandHandler : IRequestHandler<EditSalesOfficeCommand, APIResponse<EditSalesOfficeCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditSalesOfficeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<EditSalesOfficeCommandResponse>> Handle(EditSalesOfficeCommand request, CancellationToken cancellationToken)
        {
            var salesOfficesRepo = _unitOfWork.GetRepository<SalesOffice>();
            var salesOffice = await salesOfficesRepo
                .GetSource()
                .AsNoTracking()
                .Include(row => row.Address)
                .FirstOrDefaultAsync(row => !row.IsDeleted && row.ID.Equals(request.Id));

            if (salesOffice == null)
            {
                return new APIResponse<EditSalesOfficeCommandResponse>
                {
                    Errors = new List<string> { "This office is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                };
            }

            var nameExist = await salesOfficesRepo
                .GetSource()
                .AsNoTracking()
                .AnyAsync(row =>
                    !row.IsDeleted &&
                    !row.ID.Equals(request.Id) &&
                    row.OfficeName.ToLower().Equals(request.officeName.ToLower()));

            if (nameExist)
            {
                return new APIResponse<EditSalesOfficeCommandResponse>
                {
                    Errors = new List<string> { "This office name is already token!" },
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }

            salesOffice.OfficeName = request.officeName.Trim();
            salesOffice.Timestamp = DateTime.UtcNow;
            salesOffice.Address.Timestamp = DateTime.UtcNow;

            salesOffice.Address.ZipCode = request.Address.ZipCode.Trim();
            salesOffice.Address.City = request.Address.City.Trim();
            salesOffice.Address.Street = request.Address.Street.Trim();
            salesOffice.Address.State = request.Address.State.Trim();
            salesOffice.Address.BuildingNo = request.Address.BuildingNo.Trim();

            salesOfficesRepo.Update(salesOffice);
            await _unitOfWork.SaveChangesAsync();

            return new APIResponse<EditSalesOfficeCommandResponse>
            {
                Data = new EditSalesOfficeCommandResponse(request.Id),
                HttpStatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
