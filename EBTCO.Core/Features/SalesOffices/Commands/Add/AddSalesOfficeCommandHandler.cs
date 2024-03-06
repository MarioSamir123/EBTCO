using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.SalesOffices.Commands.Add
{
    public class AddSalesOfficeCommandHandler : IRequestHandler<AddSalesOfficeCommand, APIResponse<AddSalesOfficeCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddSalesOfficeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<AddSalesOfficeCommandResponse>> Handle(AddSalesOfficeCommand request, CancellationToken cancellationToken)
        {
            var salesOfficeRepo = _unitOfWork.GetRepository<SalesOffice>();
            bool SalesOfficeExist = await salesOfficeRepo
                .GetSource()
                .AsNoTracking()
                .AnyAsync(row => row.OfficeName.ToLower().Equals(request.OfficeName.ToLower()));
            
            if (SalesOfficeExist) 
            {
                return new APIResponse<AddSalesOfficeCommandResponse>
                {
                    Errors = new List<string> { "This office name is already token!" },
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }
            
            var newSalesOffice = request.Map();
            await salesOfficeRepo.AddAsync(newSalesOffice);
            await _unitOfWork.SaveChangesAsync();

            return new APIResponse<AddSalesOfficeCommandResponse>
            {
                Data = new AddSalesOfficeCommandResponse(newSalesOffice.ID),
                HttpStatusCode = System.Net.HttpStatusCode.Created,
            };
        }
    }
}
