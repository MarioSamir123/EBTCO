using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Properties.Commands.AddProperty
{
    public class AddPropertyCommandHandler : IRequestHandler<AddPropertyCommand, APIResponse<AddPropertyCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddPropertyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<AddPropertyCommandResponse>> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {
            var officeQuery = _unitOfWork.GetRepository<SalesOffice>()
                .GetSource()
                .AsNoTracking()
                .Where(row => !row.IsDeleted && row.ID.Equals(request.officeId));

            var officeExist = await officeQuery.AnyAsync();
            
            if (!officeExist)
            {
                return new APIResponse<AddPropertyCommandResponse>
                {
                    Errors = new List<string> { "This Office is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            
            var property = request.MapToDb();
            var propertyRepo = _unitOfWork.GetRepository<Property>();

            await propertyRepo.AddAsync(property);
            await _unitOfWork.SaveChangesAsync();

            int PropCount = await propertyRepo.GetSource()
                .AsNoTracking()
                .CountAsync(row => row.OfficeID.Equals(request.officeId));

            await officeQuery.ExecuteUpdateAsync(row => row.SetProperty(prop => prop.NoOfProperty, prop => PropCount));
            
            return new APIResponse<AddPropertyCommandResponse>
            {
                Data = new AddPropertyCommandResponse(property.ID),
                HttpStatusCode = System.Net.HttpStatusCode.OK,  
            };
        }
    }
}
